<script>
    import Grid from "svelte-grid";
    import gridHelp from "svelte-grid/build/helper/index.mjs";
    import Chart from "../components/Chart.svelte";

    const id = () =>
        "_" +
        Math.random()
            .toString(36)
            .substr(2, 9);

    let items = [
        gridHelp.item({
            x: 0,
            y: 0,
            w: 2,
            h: 2,
            id: id(),
            data: {
                title: "Новые посетители",
                chart: {
                    display: true,
                    type: "line",
                    data: {
                        labels: [
                            "Red",
                            "Blue",
                            "Yellow",
                            "Green",
                            "Purple",
                            "Orange"
                        ],
                        datasets: [
                            {
                                data: [12, 19, 3, 5, 2, 3],
                                backgroundColor: ["rgba(255, 99, 132, 0.2)"],
                                borderColor: ["rgba(255, 99, 132, 1)"],
                                borderWidth: 1
                            }
                        ]
                    }
                }
            }
        }),
        gridHelp.item({
            x: 2,
            y: 1,
            w: 1,
            h: 1,
            id: id(),
            data: {
                title: "Посещения",
                chart: {
                    display: true,
                    type: "line",
                    data: {
                        labels: [
                            "Red",
                            "Blue",
                            "Yellow",
                            "Green",
                            "Purple",
                            "Orange"
                        ],
                        datasets: [
                            {
                                data: [12, 19, 30, 15, 29, 31],
                                backgroundColor: ["rgba(255, 99, 132, 0.2)"],
                                borderColor: ["rgba(255, 99, 132, 1)"],
                                borderWidth: 1
                            }
                        ]
                    }
                }
            }
        }),
        gridHelp.item({
            x: 3,
            y: 0,
            w: 1,
            h: 2,
            id: id(),
            data: {
                title: "Устройства",
                chart: {
                    display: true,
                    type: "doughnut",
                    data: {
                        labels: [
                            "Red",
                            "Blue",
                            "Yellow",
                            "Green",
                            "Purple",
                            "Orange"
                        ],
                        datasets: [
                            {
                                label: "Устройства",
                                data: [12, 19, 3, 5, 2, 3],
                                backgroundColor: [
                                    "rgba(255, 99, 132, 0.2)",
                                    "rgba(54, 162, 235, 0.2)",
                                    "rgba(255, 206, 86, 0.2)",
                                    "rgba(75, 192, 192, 0.2)",
                                    "rgba(153, 102, 255, 0.2)",
                                    "rgba(255, 159, 64, 0.2)"
                                ],
                                borderColor: [
                                    "rgba(255, 99, 132, 1)",
                                    "rgba(54, 162, 235, 1)",
                                    "rgba(255, 206, 86, 1)",
                                    "rgba(75, 192, 192, 1)",
                                    "rgba(153, 102, 255, 1)",
                                    "rgba(255, 159, 64, 1)"
                                ],
                                borderWidth: 1
                            }
                        ]
                    }
                }
            }
        }),
        gridHelp.item({
            x: 0,
            y: 2,
            w: 4,
            h: 2,
            id: id(),
            data: {
                title: "Источники трафика",
                chart: {
                    display: true,
                    type: "bar",
                    data: {
                        labels: [
                            "Поиск",
                            "Прямой заход",
                            "Реклама",
                            "Переход с другого сайта",
                            "Соц. сети",
                            "Другое"
                        ],
                        datasets: [
                            {
                                label: "Устройства",
                                data: [12, 19, 3, 5, 3, 3],
                                backgroundColor: [
                                    "rgba(255, 99, 132, 0.2)",
                                    "rgba(54, 162, 235, 0.2)",
                                    "rgba(255, 206, 86, 0.2)",
                                    "rgba(75, 192, 192, 0.2)",
                                    "rgba(153, 102, 255, 0.2)",
                                    "rgba(255, 159, 64, 0.2)"
                                ],
                                borderColor: [
                                    "rgba(255, 99, 132, 1)",
                                    "rgba(54, 162, 235, 1)",
                                    "rgba(255, 206, 86, 1)",
                                    "rgba(75, 192, 192, 1)",
                                    "rgba(153, 102, 255, 1)",
                                    "rgba(255, 159, 64, 1)"
                                ],
                                borderWidth: 1
                            }
                        ]
                    }
                }
            }
        }),
        gridHelp.item({
            x: 0,
            y: 4,
            w: 4,
            h: 2,
            id: id(),
            data: {
                title: "Сессии",
                chart: {
                    display: true,
                    type: "table",
                    data: {
                        table: [
                            ["10%", "политех"],
                            ["10%", "Саратов политех"],
                            ["10%", "технический унивеситет саратов"],
                            ["10%", "сгту примемная комиссия"],
                            ["10%", "сгту"]
                        ]
                    }
                }
            }
        }),
        gridHelp.item({
            x: 2,
            y: 0,
            w: 1,
            h: 1,
            id: id(),
            data: {
                title: "Доля отказов",
                chart: {
                    display: false,
                    type: "line",
                    data: {
                        labels: [
                            "Поиск",
                            "Прямой заход",
                            "Реклама",
                            "Переход с другого сайта",
                            "Соц. сети",
                            "Другое"
                        ],
                        datasets: [
                            {
                                label: "Устройства",
                                data: [12, 19, 3, 5, 3, 3],
                                backgroundColor: [
                                    "rgba(255, 99, 132, 0.2)",
                                    "rgba(54, 162, 235, 0.2)",
                                    "rgba(255, 206, 86, 0.2)",
                                    "rgba(75, 192, 192, 0.2)",
                                    "rgba(153, 102, 255, 0.2)",
                                    "rgba(255, 159, 64, 0.2)"
                                ],
                                borderColor: [
                                    "rgba(255, 99, 132, 1)",
                                    "rgba(54, 162, 235, 1)",
                                    "rgba(255, 206, 86, 1)",
                                    "rgba(75, 192, 192, 1)",
                                    "rgba(153, 102, 255, 1)",
                                    "rgba(255, 159, 64, 1)"
                                ],
                                borderWidth: 1
                            }
                        ]
                    }
                }
            }
        })
    ];
</script>

<style>
    .content {
        width: 100%;
        height: 100%;
        color: black;
    }
    :global(.svlt-grid-shadow) {
        background: pink;
    }
    :global(.svlt-grid-container) {
        width: 100%;
    }
</style>
<Grid bind:items cols={4} gap={8} let:item rowHeight={192}>
    <div class="content rounded bg-white shadow-md p-1 relative flex flex-col ">
        <h2 class="text-lg font-bold mt-3 mx-3 z-50 bg-white">
            {item.data.title}
        </h2>
        {#if item.data.chart.type != 'doughnut' && item.data.chart.type != 'bar' && item.data.chart.type != 'table'}
            <!-- content here -->
            <h3 class="text-3xl mx-auto my-auto">
                {item.data.chart.data.datasets[0].data[item.data.chart.data.datasets[0].data.length - 1]}
            </h3>
        {/if}
        {#if item.data.chart.display}
            <!-- content here -->
            <div class="absolute w-full h-full top-0 left-0 overflow-auto">
                <Chart
                    type={item.data.chart.type}
                    data={item.data.chart.data} />
            </div>
        {/if}
    </div>
</Grid>
