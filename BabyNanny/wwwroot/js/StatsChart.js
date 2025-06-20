window.statsChart = window.statsChart || null;
window.trendChart = window.trendChart || null;

window.renderStatsChart = (labels, values) => {
    const ctx = document.getElementById('statsChart');
    if (!ctx) return;
    if (window.statsChart) {
        window.statsChart.destroy();
    }
    window.statsChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Average',
                data: values,
                backgroundColor: 'rgba(54, 162, 235, 0.5)'
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
};

window.renderTrendChart = (labels, values) => {
    const ctx = document.getElementById('trendChart');
    if (!ctx) return;
    if (window.trendChart) {
        window.trendChart.destroy();
    }
    window.trendChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: labels,
            datasets: [{
                label: 'Weekly Avg',
                data: values,
                borderColor: 'rgba(255, 99, 132, 1)',
                fill: false
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
};
