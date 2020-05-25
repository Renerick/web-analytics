<script>
    import {Link, router} from "yrv";
    import {PlayIcon} from "svelte-feather-icons";
    import CountLabel from "../components/CountLabel.svelte";
    import OsIcon from "../components/OsIcon.svelte";
    import BrowserIcon from "../components/BrowserIcon.svelte";
    import DeviceIcon from "../components/DeviceIcon.svelte";

    export let row;
</script>

<style>
    td {
        @apply px-6 py-4 whitespace-no-wrap border-b border-gray-200;
    }
</style>

<td class="hidden lg:table-cell ">
    <div on:click|stopPropagation class="flex">
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
<td>
    <div class="flex">
        <DeviceIcon class="flex-shrink-0 mr-2" size="24" systemType="{row.deviceInfo.deviceType}"/>
        <OsIcon class="flex-shrink-0 h-5 w-5 mr-2" size="24"
                systemType="{row.deviceInfo.operatingSystem.type}"/>
        <BrowserIcon class="flex-shrink-0 h-5 w-5 mr-2" size="24"
                     browserType="{row.deviceInfo.browser.type}"/>
    </div>
</td>
