var domainValues = @Html.Raw(JsonSerializer.Serialize(ViewBag.Domains))
var domainCountValue = @Html.Raw(JsonSerializer.Serialize(ViewBag.DomainCount))
var doughnutChartData = {
    labels: domainValues,
    datasets: [{
        label: "Most popular Job Boards",
        borderWidth: 2,
        backgroundColor: [
            'rgb(32,178,170)',
            'rgb(28,156,149)',
            'rgb(24,135,129)'
        ],
        hoverOffset: 4,
        data: domainCountValue
    }]
}
var doughnutChartOptions = {
    plugins: {
        datalabels: {
            formatter: (value, context) => {
                return context.chart.data.labels[context.dataIndex];
            }
        }
    },
    laout: {
        padding: 20
    },
    animation: {
        animateRotate: true,
        animateScale: true,
        duration: 2000
    }
}
console.log("Below are the domain values");
console.log(domainValues, domainCountValue);
var dateStrings = @Html.Raw(JsonSerializer.Serialize(ViewBag.Dates));
var dateCount = @Html.Raw(JsonSerializer.Serialize(ViewBag.DateCount));
var dates = dateStrings.map((dateString) => {
    return new Date(dateString);
});
dates.sort((a, b) => {
    return a - b;
})
var sortedDates = dates.map((date) => {
    return date.toISOString().split('T')[0];
})
var barChartData = {
    labels: sortedDates,
    datasets: [{
        label: "Dates Applied Count",
        borderWidth: 2,
        backgroundColor: 'lightseagreen',
        data: dateCount
    }]
}
var options = {
    scales: {
        x: {
            type: 'time',
            time: {
                unit: 'month',
                displayFormats: {
                    month: 'MMM YYYY'
                }
            },
            title: {
                display: true,
                text: 'Month By Year'
            }
        },
        y: {
            beginAtZero: true
        }
    },
    animation: {
        animateRotate: true,
        animateScale: true,
        duration: 2000
    }
};
function checkInViewPort(el) {
    var rect = el.getBoundingClientRect();
    return (
        rect.top >= 0 &&
        rect.left >= 0 &&
        rect.bottom <= (window.innerHeight || document.documentElement.clientHeight) &&
        rect.right <= (window.innerWidth || document.documentElement.clientWidth)
    );
}
function handleIntersection(entries, observer) {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            startChartAnimation();
            //observer.unobserve(entry.target);
        }
    });
}
function handleDoughnutIntersection(entries, observer) {
    entries.forEach(entry => {
        if (entry.isIntersecting) {
            startDoughnutAnimation();
        }
    })
}
function startDoughnutAnimation() {
    if (window.myDoughtnut) {
        window.myDoughtnut.destroy();
    }
    var ctx = document.getElementById("piecanvas").getContext("2d");
    window.myDoughnut = new Chart(ctx, {
        type: 'doughnut',
        data: doughnutChartData,
        options: doughnutChartOptions
    })
}
function startChartAnimation() {
    if (window.myBar) {
        window.myBar.destroy();
    }
    var ctx1 = document.getElementById("barcanvas").getContext("2d");
    window.myBar = new Chart(ctx1,
        {
            type: 'bar',
            data: barChartData,
            options: options
        });
};
document.addEventListener("DOMContentLoaded", () => {
    var observer = new IntersectionObserver(handleIntersection, { threshold: 0.5 });
    var chartCanvas = document.getElementById("barcanvas");
    if (chartCanvas) {
        observer.observe(chartCanvas);
    } else {
        console.error("Unable to find the chart canvas element with ID 'barcanvas'");
    }
})
document.addEventListener("DOMContentLoaded", () => {
    var observer = new IntersectionObserver(handleDoughnutIntersection, { threshold: 0.5 })
    var doughnutCanvas = document.getElementById("piecanvas");
    if (doughnutCanvas) {
        observer.observe(doughnutCanvas);
    }
    else {
        console.error("Unable to find doughnut canvas element with ID 'piecanvas'");
    }
})

