$(document).ready(function () {
    $.get("http://localhost:3083/api/Venda/relatoriosemanal", function (data) {
        var areaChartCanvas = $("#areaChart").get(0).getContext("2d");
        var areaChart = new Chart(areaChartCanvas);
        var areaChartData = {
            labels: [
                data[5]['data'],
                data[4]['data'],
                data[3]['data'],
                data[2]['data'],
                data[1]['data'],
                data[0]['data']],
            datasets: [
                {
                    label: "Vendas",
                    fillColor: "#4b94c0",
                    strokeColor: "#4b94c0",
                    pointColor: "#4b94c0",
                    pointStrokeColor: "#3b8bba",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: [
                        data[5]['total'].replace(",", "."),
                        data[4]['total'].replace(",", "."),
                        data[3]['total'].replace(",", "."),
                        data[2]['total'].replace(",", "."),
                        data[1]['total'].replace(",", "."),
                        data[0]['total'].replace(",", ".")]
                },
                {
                    label: "",
                    data: []
                }
            ]
        };
        var areaChartOptions = {
            showScale: true,
            scaleShowGridLines: false,
            scaleGridLineColor: "rgba(0,0,0,.05)",
            scaleGridLineWidth: 1,
            scaleShowHorizontalLines: true,
            scaleShowVerticalLines: true,
            bezierCurve: true,
            bezierCurveTension: 0.3,
            pointDot: false,
            pointDotRadius: 4,
            pointDotStrokeWidth: 1,
            pointHitDetectionRadius: 20,
            datasetStroke: true,
            datasetStrokeWidth: 1,
            datasetFill: true,
            legendTemplate: "<ul class=\"<%=name.toLowerCase()%>-legend\"><% for (var i=0; i<datasets.length; i++){%><li><span style=\"background-color:<%=datasets[i].lineColor%>\"></span><%if(datasets[i].label){%><%=datasets[i].label%><%}%></li><%}%></ul>",
            maintainAspectRatio: true,
            responsive: true
        };
        areaChart.Line(areaChartData, areaChartOptions);
    });
});