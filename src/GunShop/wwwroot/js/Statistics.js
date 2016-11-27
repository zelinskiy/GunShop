

function loadChartsStatistic() {

    $.get({
        url: "/Statistics/ChartSizes",
        success: function (data) {
            createPieChart("chartsChart", data, e => "Customer " + e.CustomerId, e => e.Count);
        }
    });
    
}

function loadStoragesStatistic() {

    $.get({
        url: "/Statistics/StoragesFilled",
        success: function(data) {
            createPieChart("storagesChart", data, e => "Storage " + e.StorageId, e => e.Count);
        }
    });

}

function loadManufacturersStatistic() {

    $.get({
        url: "/Statistics/ManufacturersFilled",
        success: function (data) {
            createPieChart("manufacturersChart", data, e => "Manufacturer " + e.ManufacturerId, e => e.Count);
        }
    });

}


function createPieChart(elementId, data, labelsSelector, dataSelector) {
    var labels = data.map(labelsSelector);
    var counts = data.map(dataSelector);
    var ctx = document.getElementById(elementId);
    return new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                data: counts,
                backgroundColor: labels.map(e => getRandomColor())
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: true,
        }
    });
}

function getRandomColor() {
    var letters = '0123456789ABCDEF'.split('');
    var color = '#';
    for (var i = 0; i < 6; i++) {
        color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
}

(function () {
    loadChartsStatistic();
    loadStoragesStatistic();
    loadManufacturersStatistic();

})();



