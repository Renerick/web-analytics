<script>
    import SvelteTable from "svelte-table";
    import {onMount} from "svelte";
    import {Link, router} from "yrv";
    import ky from "ky";
    import {PlayIcon, MapIcon} from "svelte-feather-icons";

    let iconAsc = "↑";
    let iconDesc = "↓";
    let sessions = [];
    let loading = true;

    $: (async () => {
        loading = true;
        sessions = await ky
                .get(
                        "http://localhost:5000/api/v1/site/" +
                        $router.params.siteId +
                        "/pages"
                )
                .json();
        loading = false;
    })();

    // define column configs
    const columns = [
        {
            key: "view",
            title: "",
            value: v => "",
            sortable: false,
            headerClass: "px-6 py-3 border-b border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
        },
        {
            key: "dateTime",
            title: "Страница",
            value: v => v.start,
            sortable: true,
            headerClass: "px-6 py-3 border-b border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
        },
        {
            key: "sessionCount",
            title: "Количество сессий",
            value: v => v.sessionCount,
            renderValue: v => v.sessionCount,
            sortable: true,
            headerClass: "px-6 py-3 border-b border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
        }
    ];
</script>


<div class="flex flex-col">
    <div class="-my-2 py-2 overflow-x-auto sm:-mx-6 sm:px-6 lg:-mx-8 lg:px-8">
        <div class="align-middle inline-block min-w-full shadow overflow-hidden sm:rounded-lg border-b border-gray-200">
            {#if !loading && sessions.length === 0}
                <div class="w-full text-center text-gray-600">Ни одной сессии пока не было записано</div>
            {:else if !loading}

                <SvelteTable
                        classNameTable="min-w-full"
                        {columns}
                        rows={sessions}>

                    <tr class="bg-white hover:bg-gray-100 cursor-pointer"
                        slot="row"
                        let:row
                        let:n>
                        <td class="px-6 py-4 whitespace-no-wrap border-b border-gray-200">
                            <div class="flex">
                                <Link href={`/site/${$router.params.siteId}/pages/${encodeURIComponent(row.url)}/heatmap`}
                                      class={'flex justify-center p-3 rounded-full hover:bg-gray-300 text-center w-10'}>
                                    <MapIcon/>
                                </Link>
                            </div>
                        </td>
                        <td class="px-6 py-4 whitespace-no-wrap border-b border-gray-200">{row.url}</td>
                        <td class="px-6 py-4 whitespace-no-wrap border-b border-gray-200">{row.visits}</td>
                    </tr>
                </SvelteTable>
            {/if}
        </div>
    </div>
</div>

