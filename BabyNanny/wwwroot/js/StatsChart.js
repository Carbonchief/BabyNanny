window.statsChart = window.statsChart || null;

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
