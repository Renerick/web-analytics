<script>
    import SvelteTable from "svelte-table";
    import {onMount} from "svelte";
    import {Link, router} from "yrv";
    import ky from "ky";
    import {PlayIcon, MapIcon, FrownIcon} from "svelte-feather-icons";
    import Stats from "../components/Stats.svelte";
    import Loader from "../components/Loader.svelte";

    let iconAsc = "↑";
    let iconDesc = "↓";
    let sessions = [];
    let loading = true;
    let error = false;

    $: (async () => {
        loading = true;
        try {
            sessions = await ky
                    .get(
                            "http://localhost:5000/api/v1/site/" +
                            $router.params.siteId +
                            "/sessions"
                    )
                    .json();
        } catch (e) {
            error = true;
        }
        loading = false;
    })();

    // define column configs
    const columns = [
        {
            key: "view",
            title: "",
            value: v => "",
            sortable: false,
            headerClass: "px-6 py-3 border-b border-t border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
        },
        {
            key: "dateTime",
            title: "Начало сессии",
            value: v => v.start,
            sortable: true,
            headerClass: "px-6 py-3 border-b border-t border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
        },
        {
            key: "duration",
            title: "Длительность",
            value: v => v.duration,
            sortable: true,
            headerClass: "px-6 py-3 border-b border-t border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
        },
        {
            key: "activity",
            title: "Активность",
            value: v => v.activityLevel,
            renderValue: v => v.activityLevel, // capitalize
            sortable: true,
            headerClass: "px-6 py-3 border-b border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
        }
    ];
</script>

<div class="flex flex-col">
    <div class="-my-2 py-2 overflow-x-auto sm:-mx-6 sm:px-6 lg:-mx-8 lg:px-8">
        <Stats/>
    </div>
    <div class="-my-2 py-2 overflow-x-auto sm:-mx-6 sm:px-6 lg:-mx-8 lg:px-8">
        {#if error}
            <div class="bg-white overflow-hidden shadow rounded-b-lg border-t border-gray-200">
                <div class="px-4 py-5 sm:p-6 text-gray-300  flex justify-center">
                    <FrownIcon size="48"/>
                </div>
            </div>
        {:else if !loading}
            <div class="align-middle inline-block min-w-full shadow overflow-hidden sm:rounded-b-lg border-b border-gray-200">
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
                                    <Link href={`/site/${$router.params.siteId}/sessions/${row.sessionId}/recording`}
                                          class={'flex justify-center p-3 rounded-full hover:bg-gray-300 text-center w-10'}>
                                        <PlayIcon/>
                                    </Link>
                                </div>
                            </td>
                            <td class="px-6 py-4 whitespace-no-wrap border-b border-gray-200">{row.start}</td>
                            <td class="px-6 py-4 whitespace-no-wrap border-b border-gray-200">{row.duration}</td>
                            <td class="px-6 py-4 whitespace-no-wrap border-b border-gray-200">
                            <span class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-green-100 text-green-800">
                                Active
                            </span>
                            </td>
                        </tr>
                    </SvelteTable>
                {/if}
            </div>
        {:else}
            <div class="bg-white overflow-hidden shadow rounded-b-lg border-t border-gray-200 ">
                <div class="px-4 py-5 sm:p-6  text-center">
                    <Loader/>
                </div>
            </div>
        {/if}
    </div>
</div>
