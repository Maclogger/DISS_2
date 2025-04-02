let charts = {};
let updateQueue = {};

export function initializeChart(canvasId, config) {
    const ctx = document.getElementById(canvasId).getContext("2d");
    charts[canvasId] = new Chart(ctx,
        {
            type: "line",
            data: {
                datasets: [
                    {
                        label: "Spodný 95% IS",
                        data: [],
                        borderColor: "rgba(0,0,0,1)",
                        backgroundColor: "rgba(0, 0, 0, 0.7)",
                        fill: false,
                        tension: 0.1,
                        parsing: false,
                        pointRadius: 0,
                        //borderDash: [5, 5],
                    },
                    {
                        label: config.title,
                        data: [],
                        borderColor: "rgb(13,110,253)",
                        backgroundColor: "rgb(13,110,253)",
                        fill: false,
                        tension: 0.1,
                        parsing: false,
                        pointRadius: 0,
                        borderWidth: 2,
                    },
                    {
                        label: "Horný 95% IS",
                        data: [],
                        borderColor: "rgb(220,53,69)",
                        backgroundColor: "rgb(220,53,69)",
                        fill: false,
                        tension: 0.1,
                        parsing: false,
                        pointRadius: 0,
                        //borderDash: [5, 5],
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
    const resizeHandler = () => resizeCanvas(canvasId);
    charts[canvasId].resizeHandler = resizeHandler;
    window.addEventListener('resize', resizeHandler);
    resizeCanvas(canvasId);
}

let pendingUpdate = false;

export async function addDataPoint(canvasId, lower, value, upper) {
    try {
        if (!(canvasId in charts)) return;
        const chart = charts[canvasId];
        const newIndex = chart.data.datasets[1].data.length;

        chart.data.datasets[0].data.push({x: newIndex, y: lower});
        chart.data.datasets[1].data.push({x: newIndex, y: value});
        chart.data.datasets[2].data.push({x: newIndex, y: upper});

        if (!updateQueue[canvasId]) {
            updateQueue[canvasId] = true;
            requestAnimationFrame(() => {
                if (charts[canvasId]) {
                    charts[canvasId].update("none");
                }
                updateQueue[canvasId] = false;
            });
        }
    } catch (error) {
        console.error("Error updating chart:", error);
    }
}

export function resizeCanvas(canvasId) {
    const canvas = document.getElementById(canvasId);
    const parent = canvas.parentElement;
    canvas.width = parent.clientWidth;
    canvas.height = parent.clientHeight;
    const chart = charts[canvasId];
    chart.update("none");
}

export function disposeChart(canvasId) {
    if (canvasId in charts) {
        window.removeEventListener('resize', () => resizeCanvas(canvasId));
        charts[canvasId].destroy();
        delete charts[canvasId];
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
