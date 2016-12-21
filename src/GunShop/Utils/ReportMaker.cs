using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GunShop.ViewModels.LocationViewModels;

namespace GunShop.Utils
{
    public class ReportMaker
    {

        public string TempFolder { get; set; }

        public ReportMaker(string tempFolderPath)
        {
            TempFolder = tempFolderPath;
        }
        

        public string MakeShipping(ShippingViewModel shipping)
        {
            var HEADER = $@"
\documentclass{{article}} 

\usepackage[utf8]{{inputenc}}
\usepackage[russian]{{babel}}

\begin{{document}}

\hfil{{\Huge\bf Gun Shop Inc.}}\hfil
\bigskip
\hrule
\bigskip

123 Broadway \hfill (000) 111-1111 \\
City, State 12345 \hfill customers@gunshop.com\\ \\

\hfill{{\bf Shippment {shipping.ShippingId}}}\hfill
\bigskip

{{\bf From Storage:}}   {shipping.StorageA.Name.EscapeLatex()}({shipping.StorageA.Adress.EscapeLatex()})\\
{{\bf To Storage:}}  {shipping.StorageB.Name.EscapeLatex()}({shipping.StorageB.Adress.EscapeLatex()}) \\
{{\bf Author:}}  {shipping.AuthorId} \\ 
{{\bf Date:}}  {shipping.Date.ToString().EscapeLatex()} \\";

            var FOOTER = @"\end{document}";

            var TABLE = @"
\begin{center}
\begin{tabular}{ c c c c c c c }
  Item & Id & Model & Manufacturer & Qty & Price & Total  \\[2ex]
\hline\\
";
            var i = 1;
            foreach (var group in shipping.Commodities.GroupBy(c => c.CommodityTypeId))
            {
                var row = group.First();
                TABLE += $@" {i} & {row.Id} & {row.Model.EscapeLatex()}"+
                   $@"& {row.ManufacturerName.EscapeLatex()}({row.ManufacturerCountry.EscapeLatex()})" +
                   $@"& {group.Count()} & 12.50 & {group.Count() * 12.5} \\ [2ex]";
                i++;
            }

            TABLE += $@"\hline\\&&&&&Total: {shipping.Commodities.Sum(c => 12.5)}&\end{{tabular}}\end{{center}}";

            var texDocument = HEADER + TABLE + FOOTER;
            return SavePdfAndLatex(shipping.ShippingId.ToString(), "shipping", TempFolder, texDocument);

        }


        public string MakeInventory(StorageBO storage)
        {
            var HEADER = $@"
\documentclass{{article}} 

\usepackage[utf8]{{inputenc}}
\usepackage[russian]{{babel}}

\begin{{document}}

\hfil{{\Huge\bf Gun Shop Inc.}}\hfil
\bigskip
\hrule
\bigskip

123 Broadway \hfill (000) 111-1111 \\
City, State 12345 \hfill customers@gunshop.com\\ \\

\hfill{{\bf Storage {storage.Id}}}\hfill
\bigskip

{{\bf Name:}}   {storage.Name.EscapeLatex()}\\
{{\bf Adress:}}  {storage.Adress.EscapeLatex()} \\
{{\bf Coordinates:}}  {storage.Coordinates.EscapeLatex()} \\ ";

            var FOOTER = @"\end{document}";

            var TABLE = @"
\begin{center}
\begin{tabular}{ c c c c c c c }
  Item & Id & Model & Manufacturer & Qty & Price & Total  \\[2ex]
\hline\\
";
            var i = 1;
            foreach (var group in storage.StoredCommodities.GroupBy(c => c.CommodityTypeId))
            {
                var row = group.First();
                TABLE += $@" {i} & {row.Id} & {row.Model.EscapeLatex()}" +
                   $@"& {row.ManufacturerName.EscapeLatex()}({row.ManufacturerCountry.EscapeLatex()})" +
                   $@"& {group.Count()} & 12.50 & {group.Count() * 12.5} \\ [2ex]";
                i++;
            }

            TABLE += $@"\hline\\&&&&&Stored Total: {storage.StoredCommodities.Sum(c => 12.5)}&\\[2ex]";
            TABLE += $@"\hline\\&&&&&Stored Count: {storage.StoredCommodities.Count()}&\\[2ex]";
            TABLE += $@"\end{{tabular}}\end{{center}}";

            var texDocument = HEADER + TABLE + FOOTER;

            return SavePdfAndLatex(storage.Id.ToString(), "inventory", TempFolder, texDocument);
        }

        static string SavePdfAndLatex(string id, string type, string folder, string texText)
        {
            var texFilePath = Path.Combine(folder, $"{type}{id}.tex");

            File.WriteAllText(texFilePath, texText);
            Process.Start("pdflatex", $"{texFilePath} -job-name={type}{id} -output-directory={folder}");

            return Path.Combine(folder, $"{type}{id}");

        }


    }

    public static class LatexStringExtension
    {
        public static string EscapeLatex(this string str)
        {

            var chars = new Dictionary<char, string>
            {
                {'#',"\\#"},
                {'$',"\\$"},
                {'%',"\\%"},
                {'&',"\\&"},
                {'~',"\\~{}"},
                {'_',"\\_"},
                {'^',"\\^{}"},
                {'\\',"\\textbackslash"},
                {'{',"\\{"},
                {'}',"\\}"}
            };

            return string.Join("",str.Select(c => chars.ContainsKey(c)?chars[c]:c.ToString()));
        }
    }
}
