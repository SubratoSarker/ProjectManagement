// Dashboard charts (amCharts 5, loaded globally via index.js/percent.js/xy.js/Animated.js in App.razor).
// Those CDN scripts are tagged `async`, and this page calls into them immediately on first render
// (OnAfterRenderAsync(firstRender)) -- unlike the app's other chart pages, which only call into amCharts
// after a user clicks a button, by which time the async scripts have long since finished loading.
// So on a fresh/direct load of /Dashboard, am5/am5percent/am5xy can still be undefined at call time.
// This polls for them instead of assuming they're ready.
window.__pmCharts = window.__pmCharts || {};

function disposePmChart(divId) {
    if (window.__pmCharts[divId]) {
        window.__pmCharts[divId].dispose();
        delete window.__pmCharts[divId];
    }
}

function whenAmChartsReady(callback, attemptsLeft) {
    if (typeof attemptsLeft === "undefined") attemptsLeft = 100; // ~5s at 50ms
    if (window.am5 && window.am5percent && window.am5xy && window.am5themes_Animated) {
        callback();
    } else if (attemptsLeft > 0) {
        setTimeout(function () { whenAmChartsReady(callback, attemptsLeft - 1); }, 50);
    } else {
        console.error("amCharts 5 libraries never became available (am5/am5percent/am5xy/am5themes_Animated).");
    }
}

function showPmChartError(divId, err) {
    console.error("Dashboard chart error for #" + divId + ":", err);
    var el = document.getElementById(divId);
    if (el) {
        el.innerHTML = '<div class="text-danger small">Chart failed to render: ' + (err && err.message ? err.message : err) + '</div>';
    }
}

function createStatusPieChart(divId, jsonData) {
    disposePmChart(divId);
    whenAmChartsReady(function () {
        try {
            var data = JSON.parse(jsonData);
            am5.ready(function () {
                var root = am5.Root.new(divId);
                window.__pmCharts[divId] = root;
                root.setThemes([am5themes_Animated.new(root)]);

                var chart = root.container.children.push(am5percent.PieChart.new(root, {
                    innerRadius: am5.percent(40)
                }));

                var series = chart.series.push(am5percent.PieSeries.new(root, {
                    valueField: "TaskCount",
                    categoryField: "strStatus"
                }));

                series.data.setAll(data);
                series.appear(800, 100);

                var legend = chart.children.push(am5.Legend.new(root, {
                    centerX: am5.percent(50),
                    x: am5.percent(50)
                }));
                legend.data.setAll(series.dataItems);
            });
        } catch (err) {
            showPmChartError(divId, err);
        }
    });
}

function createBottleneckFunnel(divId, jsonData) {
    disposePmChart(divId);
    whenAmChartsReady(function () {
        try {
            var data = JSON.parse(jsonData);
            am5.ready(function () {
                var root = am5.Root.new(divId);
                window.__pmCharts[divId] = root;
                root.setThemes([am5themes_Animated.new(root)]);

                var chart = root.container.children.push(am5percent.SlicedChart.new(root, {}));

                var series = chart.series.push(am5percent.FunnelSeries.new(root, {
                    alignLabels: true,
                    categoryField: "Stage",
                    valueField: "Count"
                }));

                // Force high-contrast label/tick colors instead of relying on amCharts' theme
                // defaults, which can end up close to this card's own background/text color.
                series.labels.template.setAll({
                    fill: am5.color(0x212529),
                    fontSize: 13
                });
                series.ticks.template.setAll({
                    stroke: am5.color(0x6c757d)
                });
                series.slices.template.setAll({
                    tooltipText: "{category}: {value}"
                });

                series.data.setAll(data);
                series.appear(800, 100);
            });
        } catch (err) {
            showPmChartError(divId, err);
        }
    });
}

function createGanttChart(divId, jsonData) {
    disposePmChart(divId);
    whenAmChartsReady(function () {
        try {
            var data = JSON.parse(jsonData);
            am5.ready(function () {
                var root = am5.Root.new(divId);
                window.__pmCharts[divId] = root;
                root.setThemes([am5themes_Animated.new(root)]);

                var chart = root.container.children.push(am5xy.XYChart.new(root, {
                    panX: true,
                    panY: true,
                    wheelX: "panX",
                    wheelY: "zoomX"
                }));

                var yAxis = chart.yAxes.push(am5xy.CategoryAxis.new(root, {
                    categoryField: "Task",
                    renderer: am5xy.AxisRendererY.new(root, { inversed: true })
                }));
                yAxis.data.setAll(data);

                var xAxis = chart.xAxes.push(am5xy.DateAxis.new(root, {
                    baseInterval: { timeUnit: "day", count: 1 },
                    renderer: am5xy.AxisRendererX.new(root, {})
                }));

                var series = chart.series.push(am5xy.ColumnSeries.new(root, {
                    xAxis: xAxis,
                    yAxis: yAxis,
                    categoryYField: "Task",
                    openValueXField: "Start",
                    valueXField: "End",
                    tooltip: am5.Tooltip.new(root, { labelText: "{categoryY}: {openValueX} - {valueX}" })
                }));

                series.columns.template.setAll({
                    height: am5.percent(60),
                    fill: am5.color(0x0d6efd),
                    stroke: am5.color(0x0d6efd)
                });
                series.data.setAll(data);
                series.appear(800, 100);
                chart.appear(800, 100);
            });
        } catch (err) {
            showPmChartError(divId, err);
        }
    });
}

function createWorkloadBarChart(divId, jsonData) {
    disposePmChart(divId);
    whenAmChartsReady(function () {
        try {
            var data = JSON.parse(jsonData);
            am5.ready(function () {
                var root = am5.Root.new(divId);
                window.__pmCharts[divId] = root;
                root.setThemes([am5themes_Animated.new(root)]);

                var chart = root.container.children.push(am5xy.XYChart.new(root, {
                    panX: false,
                    panY: false,
                    wheelX: "none",
                    wheelY: "none"
                }));

                var xAxis = chart.xAxes.push(am5xy.CategoryAxis.new(root, {
                    categoryField: "strUserName",
                    renderer: am5xy.AxisRendererX.new(root, {})
                }));
                xAxis.data.setAll(data);

                var yAxis = chart.yAxes.push(am5xy.ValueAxis.new(root, {
                    renderer: am5xy.AxisRendererY.new(root, {})
                }));

                var series = chart.series.push(am5xy.ColumnSeries.new(root, {
                    xAxis: xAxis,
                    yAxis: yAxis,
                    valueYField: "OpenTaskCount",
                    categoryXField: "strUserName",
                    tooltip: am5.Tooltip.new(root, { labelText: "{categoryX}: {valueY}" })
                }));

                series.data.setAll(data);
                series.appear(800, 100);
                chart.appear(800, 100);
            });
        } catch (err) {
            showPmChartError(divId, err);
        }
    });
}
