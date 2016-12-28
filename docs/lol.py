KEY = """CategoriesId
CharacteristicsId
CharacteristicValuesCommodityTypeId
CharacteristicValuesCharacteristicId
CommoditiesId
CommoditiesInChartsCommodityId
CommoditiesInChartsCustomerId
CommoditiesTypesId
CommoditiesTypesInCathegoriesCommodityTypeId
CommoditiesTypesInCathegoriesCategoryId
CustomersId
ManufacturersId
OrdersId
ShippingRowsCommodityId
ShippingRowsShippingId
ShippingsId
ShopsId
StorageId"""
NK = """CategoriesMasterCategoryId
CategoriesName
CharacteristicsAvailableValues
CharacteristicsName
CharacteristicsType
CharacteristicsCategoryId
CharValuesValue
CommoditiesCommodityTypeId
CommoditiesOrderId
CommoditiesStorageId
CommoditiesTypesModel
CommoditiesTypesSize
CommoditiesTypesWeight
CommoditiesTypesManufacturerId
CommoditiesTypesPrice
CustomersApplicationUserId
CustomersEmail
CustomersIsTemp
ManufacturersCountry
ManufacturersName
OrdersCustomerId
OrdersDateTime
ShippingsAuthorId
ShippingsDate
ShippingsStorageAId
ShippingsStorageBId
ShopsAdress
ShopsCoordinates
ShopsName
ShopsStorageId
StorageAdress
StorageCoordinates
StorageName"""
B="""CAT.Id AS CategoriesId,
CAT.MasterCategoryId AS CategoriesMasterCategoryId,
CAT.Name AS CategoriesName,
CHR.Id AS CharacteristicsId,
CHR.AvailableValues AS CharacteristicsAvailableValues,
CHR.Name AS CharacteristicsName,
CHR.Type AS CharacteristicsType,
CHR.CategoryId AS CharacteristicsCategoryId,
CV.CommodityTypeId AS CharacteristicValuesCommodityTypeId,
CV.CharacteristicId AS CharacteristicValuesCommodityTypeId,
CV.Value AS CharValuesValue,
C.Id AS CommoditiesId,
C.CommodityTypeId AS CommoditiesCommodityTypeId,
C.OrderId AS CommoditiesOrderId,
C.StorageId AS CommoditiesStorageId,
CIC.CommodityId AS CommoditiesInChartsCommodityId,
CIC.CustomerId AS CommoditiesInChartsCustomerId,
CT.Id AS CommoditiesTypesId,
CT.Model AS CommoditiesTypesModel,
CT.Size AS CommoditiesTypesSize,
CT.Weight AS CommoditiesTypesWeight,
CT.ManufacturerId AS CommoditiesTypesManufacturerId,
CT.Price AS CommoditiesTypesPrice,
CTIC.CommodityTypeId AS CommoditiesTypesInCathegoriesCommodityTypeId,
CTIC.CategoryId AS CommoditiesTypesInCathegoriesCategoryId,
CUST.Id AS CustomersId,
CUST.ApplicationUserId AS CustomersApplicationUserId,
CUST.Email AS CustomersEmail,
CUST.IsTemp AS CustomersIsTemp,
M.Id AS ManufacturersId,
M.Country AS ManufacturersCountry,
M.Name AS ManufacturersName,
ORD.Id AS OrdersId,
ORD.CustomerId AS OrdersCustomerId,
ORD.DateTime AS OrdersDateTime,
SR.CommodityId AS ShippingRowsCommodityId,
SR.ShippingId AS ShippingRowsShippingId,
SS.Id AS ShippingsId,
SS.AuthorId AS ShippingsAuthorId,
SS.Date AS ShippingsDate,
SS.StorageAId AS ShippingsStorageAId,
SS.StorageBId AS ShippingsStorageBId,
SH.Id AS ShopsId,
SH.Adress AS ShopsAdress,
SH.Coordinates AS ShopsCoordinates,
SH.Name AS ShopsName,
SH.StorageId AS ShopsStorageId,
ST.Id AS StorageId,
ST.Adress AS StorageAdress,
ST.Coordinates AS StorageCoordinates,
ST.Name AS StorageName"""
print B.replace("\n"," ")

def fz():
    KEY = KEY.split("\n")
    NK = NK.split("\n")
    i=2
    for k in KEY:
        nk = filter(lambda x: x.startswith(k[:-2]), NK)
        print "\nTable 3."+str(i)+" - \"" + k + "\"\n"
        i+=1
        for x in nk:
            print x

