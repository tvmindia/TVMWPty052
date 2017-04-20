$(document).ready(function () {
    try
    {
        debugger;
        BindBarChart();
        BindPieChart();
    }
    catch(e)
    {

    }
   
});
function GetWeeklySalesDetails()
{
    try {
        var ds = {};
        data = "";
        ds = GetDataFromServer("DashBoard/GetWeeklySalesDetails/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {

    }
}
function GetRootCategoryWiseSalesDetail()
{
    try {
        var ds = {};
        data = "";
        ds = GetDataFromServer("DashBoard/GetRootCategoryWiseSalesDetail/", data);
        if (ds != '') { ds = JSON.parse(ds); }
        if (ds.Result == "OK") { return ds.Records; }
        if (ds.Result == "ERROR") { alert(ds.Message); }
    }
    catch (e) {

    }
}
function BindBarChart()
{
    try
    {
        var chartjsData = [];
        var labels = [];
        var BarGraphData = GetWeeklySalesDetails()
        for (var i = 0; i < BarGraphData.length; i++) {
            labels.push(BarGraphData[i].Label);
            chartjsData.push(BarGraphData[i].Value);
        }
        var ctx = document.getElementById("myChart");
        var myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: labels, //["Mar 1st Week", "Mar 2nd Week", "Mar 3rd Week", "Mar 4th Week", "Apr 1st Week", "Apr 2nd Week"],
                datasets: [{
                    label: 'PartyEC Sales',
                    data: chartjsData,// [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 106, 76, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 2
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }

            }
        });
    }
    catch(e)
    {

    }
    
}
function BindPieChart()
{

    var chartjsData = [];
    var labels = [];
    var PieGraphData = GetRootCategoryWiseSalesDetail()
    for (var i = 0; i < PieGraphData.length; i++) {
        labels.push(PieGraphData[i].Label);
        chartjsData.push(PieGraphData[i].Value);
    }
    var ctxpi = document.getElementById("myChartpi");
    var myPieChart = new Chart(ctxpi, {
        type: 'pie',
        data: {
            labels: labels,//[
            //    "Marriage Cake",
            //    "Flower",
            //    "Birthday Cake",
            //    "Video"
            //],
            datasets: [
                {
                    data: chartjsData,//[300, 50, 100, 80],
                    backgroundColor: [
                        "#FF6384",
                        "#36A2EB",
                        "#FFCE56",
                        "#3a3a3a"
                    ],
                    hoverBackgroundColor: [
                        "#FF6384",
                        "#36A2EB",
                        "#FFCE56"
                    ]
                }]
        },
        options: {
            animation: {
                animateScale: true
            }
        }
    });
}