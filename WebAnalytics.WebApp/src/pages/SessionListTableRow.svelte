<script>
    import {Link, router} from "yrv";
    import {PlayIcon} from "svelte-feather-icons";
    import CountLabel from "../components/CountLabel.svelte";

    export let row;
</script>

<style>
    td {
        @apply px-6 py-4 whitespace-no-wrap border-b border-gray-200;
    }
</style>

<td class="hidden lg:table-cell ">
    <div class="flex">
        <Link href={`/site/${$router.params.siteId}/sessions/${row.sessionId}/recording`}
              class={'flex justify-center p-3 rounded-full hover:bg-gray-300 text-center w-10'}>
            <PlayIcon/>
        </Link>
    </div>
</td>
<td class="">{row.start}</td>
<td class="">
    <div class="leading-5 text-gray-900">{row.duration}</div>
    <div class="text-sm leading-5 text-gray-500">
        <CountLabel count="{row.recordingFragments.length}" forms={["страница", "страницы", "страниц"]}/>
    </div>
</td>
<td class="">
    <span class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full bg-green-100 text-green-800">
        Active
    </span>
</td>
