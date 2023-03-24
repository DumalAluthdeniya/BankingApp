let nav__link = document.getElementsByClassName('nav__link');
let ArrayLinks=Array.from(nav__link);
ArrayLinks.forEach(function(link) {
    link.addEventListener('click', function(e) {
        (e.target.classList.contains('logo'))? e.target.classList.add('text-blue'): SetLinkActive(e.target);
    });
})

function SetLinkActive(target) {
    ArrayLinks.forEach(function(link) {
        (link != target)? link.classList.remove('nav__link__active'):link.classList.add('nav__link__active')
    })
}



let tab__links = document.getElementsByClassName('nav-item');
let ArrayTabLinks=Array.from(tab__links);
ArrayTabLinks.forEach(function(link) {
    link.addEventListener('click', function(e) {
        SetTabActive(e.target);
    });
})

function SetTabActive(target) {
    ArrayTabLinks.forEach(function(link) {
        (link != target)? link.classList.remove('nav-item-active'):link.classList.add('nav-item-active')
    })
}


let hamburger__icon = document.getElementById('hamburger__icon');
let hide__sidebar = document.getElementById('hide__sidebar');
let sidebar = document.getElementById('sidebar__menu');
hamburger__icon.addEventListener('click', () =>{
    hide__sidebar.classList.remove('d-none');
    hide__sidebar.classList.add('d-block');
    sidebar.classList.remove('d-none');
    sidebar.classList.add('d-block');
});

hide__sidebar.addEventListener('click', () =>{
    hide__sidebar.classList.add('d-none');
    hide__sidebar.classList.remove('d-block');
    sidebar.classList.add('d-none');
    sidebar.classList.remove('d-block');
});


const ctx = document.getElementById('myChart');
if(ctx){
    new Chart(ctx, {
        type: 'line',
        data: {
          labels: ['JAN', 'FEB', 'MAR', 'APR', 'MAY', 'JUN','JUL','JAN'],
          datasets: [{
            label: '',
            data: [28, 22, 25, 23, 50, 27,37,33,42,36],
            backgroundColor:'transparent',
            borderColor:'#1573FF',
            pointBorderColor:'#1C3879',
            pointBorderWidth:6,
            tension:0.5,
            borderWidth: 4
          }]
        },
        options: {
            plugins:{
                legend: false,
                tooltip: {
                    enabled: false,
      
                    external: function(context) {
                        // Tooltip Element
                        let tooltipEl = document.getElementById('chartjs-tooltip');
      
                        // Create element on first render
                        if (!tooltipEl) {
                            tooltipEl = document.createElement('div');
                            tooltipEl.id = 'chartjs-tooltip';
                            tooltipEl.innerHTML = '<table></table>';
                            document.body.appendChild(tooltipEl);
                        }
      
                        // Hide if no tooltip
                        const tooltipModel = context.tooltip;
                        if (tooltipModel.opacity === 0) {
                            tooltipEl.style.opacity = 0;
                            return;
                        }
      
                        // Set caret Position
                        tooltipEl.classList.remove('above', 'below', 'no-transform');
                        if (tooltipModel.yAlign) {
                            tooltipEl.classList.add(tooltipModel.yAlign);
                        } else {
                            tooltipEl.classList.add('no-transform');
                        }
      
                        function getBody(bodyItem) {
                            return bodyItem.lines;
                        }
      
                        // Set Text
                        if (tooltipModel.body) {
                            const bodyLines = tooltipModel.body.map(getBody);
      
                            let innerHtml = '<thead>';
                            innerHtml += '</thead><tbody>';
      
                            bodyLines.forEach(function(body, i) {
                                const span = '<span class="chartToolTip" >$ ' + body + 'K</span>';
                                innerHtml += '<tr><td>' + span + '</td></tr>';
                            });
                            innerHtml += '</tbody>';
      
                            let tableRoot = tooltipEl.querySelector('table');
                            tableRoot.innerHTML = innerHtml;
                        }
      
                        const position = context.chart.canvas.getBoundingClientRect();
                        const bodyFont = Chart.helpers.toFont(tooltipModel.options.bodyFont);
      
                        // Display, position, and set styles for font
                        tooltipEl.style.opacity = 1;
                        tooltipEl.style.position = 'absolute';
                        tooltipEl.style.left = position.left - 45 + window.pageXOffset + tooltipModel.caretX + 'px';
                        tooltipEl.style.top = position.top - 55 + window.pageYOffset + tooltipModel.caretY + 'px';
                        tooltipEl.style.font = bodyFont.string;
                        tooltipEl.style.padding = tooltipModel.padding + 'px ' + tooltipModel.padding + 'px';
                        tooltipEl.style.pointerEvents = 'none';
                    }
                }
            },
          scales: {
            x:{
                grid:{
                    display: false
                }
            },
            y:{
                min:10,
                max:50,
                ticks:{
                    stepSize:10,
                    callback:(value) => value + 'K'
                },
                grid:{
                    borderDash:[10]
                }
            }
          }
        }
    });
}