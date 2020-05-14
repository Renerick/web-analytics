<script>
    import { Router, Route, Link, router } from "yrv";
    import { ActivityIcon, MapIcon, SettingsIcon, LayoutIcon } from "svelte-feather-icons";
    import {onMount} from 'svelte';

    $: siteId = $router.params.siteId;
    let baseUrl = "/site/:siteId";

    let items = [
        {
            label: "Dashboard",
            url: baseUrl + "/dashboard",
            icon: LayoutIcon
        },
        {
            label: "Sessions",
            url: baseUrl + "/sessions",
            icon: ActivityIcon
        },
        {
            label: "Heatmaps",
            url: baseUrl + "/heatmaps",
            icon: MapIcon
        },
        {
            label: "Site setting",
            url: baseUrl + "/settings",
            icon: SettingsIcon
        }
    ];
</script>

<nav>
    <ul>
        {#each items as item}
            <li
                class={'my-1 rounded ' + (item.url.replace(":siteId", siteId) === $router.path ? 'bg-primary text-white' : 'hover:bg-gray-300')}>
                <Link
                    href={item.url.replace(":siteId", siteId)}
                    class={'px-4 py-3 text-lg w-full h-full flex align-center'}>
                    <span>
                        <svelte:component this={item.icon} size="24" />
                    </span>
                    <span class={'ml-2'}>{item.label}</span>
                </Link>
            </li>
        {/each}
    </ul>
</nav>
