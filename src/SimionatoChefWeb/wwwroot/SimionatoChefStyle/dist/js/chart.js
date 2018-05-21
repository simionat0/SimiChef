$(function () {
    var areaGraficoCanvas = $("#areaGrafico").get(0).getContext("2d");
    var areaGrafico = new Chart(areaGraficoCanvas);
    var areaGraficoData = {
        labels: ["Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sabado"],
        datasets: [
            {
                label: "Valores",
                fillColor: "#367ba2",
                strokeColor: "#367ba2",
                pointColor: "#367ba2",
                pointStrokeColor: "#c1c7d1",
                pointHighlightFill: "#fff",
                pointHighlightStroke: "rgba(220,220,220,1)",
                data: [50.25, 100, 35, 200, 1000, 857]
            }
        ]
    };
    var areaGraficoOptions = {
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
        datasetStrokeWidth: 2,
        datasetFill: true,
        maintainAspectRatio: true,
        responsive: true
    };
    areaGrafico.Line(areaGraficoData, areaGraficoOptions);
});