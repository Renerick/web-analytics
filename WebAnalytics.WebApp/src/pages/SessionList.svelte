<script>
    import SvelteTable from "svelte-table";
    import {onMount} from "svelte";
    import {Link, router} from "yrv";
    import ky from "ky";
    import {PlayIcon, MapIcon} from "svelte-feather-icons";
    import Stats from "../components/Stats.svelte";
    import Loader from "../components/Loader.svelte";
    import SessionListTableRow from "./SessionListTableRow.svelte";
    import LoaderFrame from "../components/LoaderFrame.svelte";

    let iconAsc = "↑";
    let iconDesc = "↓";
    let sessions = [];
    let loading = true;
    let error = false;

    async function fetchData(siteId) {
        sessions = await ky
                .get(
                        "http://localhost:5000/api/v1/site/" +
                        siteId +
                        "/sessions"
                )
                .json();
    }

    // define column configs
    const columns = [
        {
            key: "view",
            title: "",
            value: v => "",
            sortable: false,
            headerClass: "hidden lg:table-cell px-6 py-3 border-b border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
        },
        {
            key: "dateTime",
            title: "Начало сессии",
            value: v => v.start,
            sortable: true,
            headerClass: "px-6 py-3 border-b border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
        },
        {
            key: "duration",
            title: "Длительность",
            value: v => v.duration,
            sortable: true,
            headerClass: "px-6 py-3 border-b border-gray-200 bg-gray-50 text-left text-xs leading-4 font-medium text-gray-500 uppercase tracking-wider"
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

<LoaderFrame func={async () => await fetchData($router.params.siteId)}>
    <div class="flex flex-col">
        <div class="mt-5 align-middle inline-block min-w-full shadow overflow-hidden sm:rounded-lg border-b border-gray-200">
            {#if sessions.length === 0}
                <div class="w-full text-center text-gray-600">Ни одной сессии пока не было записано</div>
            {:else}
                <SvelteTable
                        classNameTable="min-w-full"
                        {columns}
                        rows={sessions}>
                    <tr class=" bg-white hover:bg-gray-100 cursor-pointer"
                        slot="row"
                        let:row
                        let:n>
                        <SessionListTableRow {row}/>
                    </tr>
                </SvelteTable>
            {/if}
        </div>
    </div>
</LoaderFrame>

