

function loadChartsStatistic() {

    $.get({
        url: "/Statistics/ChartSizes",
        success: fillChartsStatistics
    });
    
}

function loadStoragesStatistic() {

    $.get({
        url: "/Statistics/StoragesFilled",
        success: fillStoragesStatistics
    });

}


function fillChartsStatistics(data) {
    console.log(data);
    var labels = data.map(e => "Customer " + e.CustomerId);
    var counts = data.map(e => e.Count);
    var ctx = document.getElementById("myChart");
    var myChart = new Chart(ctx, {
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


function fillStoragesStatistics(data) {
    console.log(data);
    var labels = data.map(e => "Storage " + e.StorageId);
    var counts = data.map(e => e.Count);
    var ctx = document.getElementById("myChart2");
    var myChart = new Chart(ctx, {
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

})();



