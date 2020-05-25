<script>
    import {MonitorIcon, TabletIcon, SmartphoneIcon, TvIcon} from 'svelte-feather-icons';

    export let systemType;
    export let size = 24;
    let className = "";
    export {className as class};
</script>

{#if systemType === "desktop"}
    <span class="{className}" ><MonitorIcon {size}/></span>
{:else if systemType === "tablet"}
    <span class="{className}" ><TabletIcon {size}/></span>
{:else if systemType === "phablet"}
    <span class="{className}" ><TabletIcon {size}/></span>
{:else if systemType === "smartphone"}
    <span class="{className}" ><SmartphoneIcon {size}/></span>
{:else if systemType === "tv"}
    <span class="{className}" ><TvIcon {size}/></span>
{/if}
