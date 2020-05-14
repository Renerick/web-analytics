<script>
    import Chart from "chart.js";
    import { onMount } from "svelte";

    export let type = "line";
    export let data;

    let chart;

    onMount(() => {
        if (type == "table" || type == "number") return;
        var ctx = chart.getContext("2d");
        var myChart = new Chart(ctx, {
            type: type,

            data: data,
            options: {
                layout: {
                    padding: {
                        left: 0,
                        right: 0,
                        top: 48,
                        bottom: 0
                    }
                },
                scales: {
                    display: false,
                    xAxes: [
                        {
                            gridLines: {
                                display: false,
                                drawTicks: false
                            },
                            ticks: {
                                display: false //this will remove only the label
                            }
                        }
                    ],
                    yAxes: [
                        {
                            gridLines: {
                                display: false,
                                drawTicks: false
                            },
                            ticks: {
                                display: false //this will remove only the label
                            }
                        }
                    ]
                },
                title: {
                    display: false
                },
                legend: {
                    display: false
                },
                responsive: true,
                maintainAspectRatio: false
            }
        });
    });
</script>

<div class="relative w-full h-full">
    {#if type != 'table'}
        <canvas bind:this={chart} />
    {:else}
        <table class="mt-12 m-4">
            {#each data.table as row}
                <tr>
                    {#each row as col}
                        <td class="p-1">{col}</td>
                    {/each}
                </tr>
            {/each}
        </table>
    {/if}
</div>
