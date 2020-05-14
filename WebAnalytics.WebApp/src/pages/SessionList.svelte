<script>
    import SvelteTable from "svelte-table";
    import { onMount } from "svelte";
    import { Link, router } from "yrv";
    import ky from "ky";
    import { PlayIcon } from "svelte-feather-icons";

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
                    "/sessions"
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
            headerClass: "px-4 py-2"
        },
        {
            key: "dateTime",
            title: "Начало",
            value: v => v.start,
            sortable: true,
            headerClass: "px-4 py-2"
        },
        {
            key: "duration",
            title: "Длительность",
            value: v => v.duration,
            sortable: true,
            headerClass: "px-4 py-2"
        },
        {
            key: "activity",
            title: "Активность",
            value: v => v.activityLevel,
            renderValue: v => v.activityLevel, // capitalize
            sortable: true,
            headerClass: "px-4 py-2"
        }
    ];
</script>

{#if !loading && sessions.length === 0}
    <div class="w-full text-center text-gray-600">Здесь пока пусто</div>
{:else if !loading}
    <SvelteTable
        classNameTable="table-fixed rounded overflow-hidden shadow"
        classNameThead="bg-gray-200 text-gray-600"
        {columns}
        rows={sessions}>
        <tr
            class="bg-white hover:bg-gray-100 cursor-pointer"
            slot="row"
            let:row
            let:n>
            <td class="px-4 py-3">
                <Link
                    href={`/site/${$router.params.siteId}/session/${row.sessionId}/recording`}
                    class={'flex justify-center p-3 rounded-full hover:bg-gray-300 text-center w-12'}>
                    <PlayIcon />
                </Link>
            </td>
            <td class="px-4 py-3">{row.start}</td>
            <td class="px-4 py-3">{row.duration}</td>
            <td class="px-4 py-3">{row.activityLevel}</td>
        </tr>
    </SvelteTable>
{/if}
