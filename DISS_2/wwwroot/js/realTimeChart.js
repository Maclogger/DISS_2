let charts = {};

export function initializeChart(canvasId, config) {
    const ctx = document.getElementById(canvasId).getContext("2d");
    charts[canvasId] = new Chart(ctx, {
        type: "line",
        data: {
            datasets: [
                {
                    label: "Lower Bound",
                    data: [],
                    borderColor: "rgba(192, 75, 75, 0.5)",
                    backgroundColor: "rgba(0, 0, 0, 0)",
                    fill: false,
                    tension: 0.1,
                    parsing: false,
                    borderDash: [5, 5],
                },
                {
                    label: config.title,
                    data: [],
                    borderColor: "rgb(13,110,253)",
                    backgroundColor: "rgb(13,110,253)",
                    fill: false,
                    tension: 0.1,
                    parsing: false,
                    borderWidth: 2,
                },
                {
                    label: "Upper Bound",
                    data: [],
                    borderColor: "rgba(192, 75, 75, 0.5)",
                    backgroundColor: "rgba(0, 0, 0, 0)",
                    fill: false,
                    tension: 0.1,
                    parsing: false,
                    borderDash: [5, 5],
                },
            ],
        },
        options: {
            responsive: false,
            scales: {
                x: {
                    type: "linear",
                    title: {
                        display: true,
                        text: config.xAxisLabel,
                    },
                },
                y: {
                    title: {
                        display: true,
                        text: config.yAxisLabel,
                    },
                },
            },
        },
    });
}

let pendingUpdate = false;

export async function addDataPoint(canvasId, lower, value, upper) {
    try {
        return new Promise((resolve, reject) => {
            if (!(canvasId in charts)) return;
            const chart = charts[canvasId];
            const newIndex = chart.data.datasets[1].data.length;

            chart.data.datasets[0].data.push({x: newIndex, y: lower});
            chart.data.datasets[1].data.push({x: newIndex, y: value});
            chart.data.datasets[2].data.push({x: newIndex, y: upper});

            if (!pendingUpdate) {
                pendingUpdate = true;
                requestAnimationFrame(() => {
                    chart.update("none");
                    pendingUpdate = false;
                });
            }
        });
    } catch (error) {
    }
}

export function reset(canvasId) {
    if (!(canvasId in charts)) return;
    const chart = charts[canvasId];
    chart.data.datasets[0].data = [];
    chart.data.datasets[1].data = [];
    chart.data.datasets[2].data = [];
    chart.update("none");
}
