<script>
    import {Router, Route, Link, router} from "yrv";
    import {ActivityIcon, MapIcon, SettingsIcon, LayoutIcon, FileTextIcon} from "svelte-feather-icons";
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
            label: "Pages",
            url: baseUrl + "/pages",
            icon: FileTextIcon
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
            <li class={'relative my-1 rounded-r-lg hover:bg-gray-300 ' + ( $router.path.startsWith(item.url.replace(":siteId", siteId)) ? 'text-primary bg-gray-200' : '')}>
                <Link href={item.url.replace(":siteId", siteId)}
                      class={'px-4 py-3 w-full h-full flex align-center focus:outline-none'}>
                    <span>
                        <svelte:component this={item.icon} size="24"/>
                    </span>
                    <span class={'ml-4'}>{item.label}</span>
                </Link>
            </li>
        {/each}
    </ul>

</nav>
