<script>
    import {Router, Route, Link, router} from "yrv";
    import {ActivityIcon, XIcon, SettingsIcon, LayoutIcon, FileTextIcon} from "svelte-feather-icons";
    import SiteSelector from './SiteSelector.svelte';
    import {fade, fly, slide} from 'svelte/transition';
    import semlytics from "images/semlytics-logo-color-small.svg";


    export let mobile = false;

    let baseUrl = "/site/:siteId";
    let siteId;
    $: siteId = $router.params.siteId;

    let items = [
        // {
        //     label: "Dashboard",
        //     url: baseUrl + "/dashboard",
        //     icon: LayoutIcon
        // },
        {
            label: "Сессии",
            url: baseUrl + "/sessions",
            icon: ActivityIcon
        },
        {
            label: "Страницы",
            url: baseUrl + "/pages",
            icon: FileTextIcon
        },
        // {
        //     label: "Настройки сайта",
        //     url: baseUrl + "/settings",
        //     icon: SettingsIcon
        // }
    ];

    let mobileActive = false;

    export function open() {
        mobileActive = true;
    }
</script>

<style>

    :global(.link) {
        @apply mt-1 flex items-center px-2 py-2 text-sm leading-5 font-medium text-gray-600 rounded-md transition ease-in-out duration-150;
    }

    :global(.link:hover) {
        @apply text-gray-900 bg-gray-50;
    }

    :global(.link:focus) {
        @apply outline-none text-gray-900 bg-gray-100 ;
    }

    :global(.link[aria-current="page"]) {
        @apply mt-1 flex items-center px-2 py-2 text-sm leading-5 font-medium text-gray-900 rounded-md bg-gray-100 transition ease-in-out duration-150;
    }

    :global(.link[aria-current="page"]:hover) {
        @apply text-gray-900 bg-gray-100;
    }

    :global(.link[aria-current="page"]:focus) {
        @apply outline-none ;
        @apply bg-gray-200 ;
    }

    /*Mobile menu classes*/

    :global(.mobile-link) {
        @apply mt-1 flex items-center px-2 py-2 text-base leading-6 font-medium text-gray-600 rounded-md transition ease-in-out duration-150;
    }

    :global(.mobile-link:hover) {
        @apply text-gray-900 bg-gray-50 transition ease-in-out duration-150;
    }

    :global(.mobile-link:focus) {
        @apply outline-none text-gray-900 bg-gray-100 transition ease-in-out duration-150;
    }

    :global(.mobile-link[aria-current="page"]) {
        @apply mt-1 flex items-center px-2 py-2 text-base leading-6 font-medium text-gray-900 rounded-md bg-gray-100 transition ease-in-out duration-150;
    }

    :global(.mobile-link[aria-current="page"]:focus) {
        @apply outline-none ;
        @apply bg-gray-200 ;
    }
</style>

{#if mobile}
    {#if mobileActive}
        <div class="fixed inset-0 flex z-40">
            <!--
              Off-canvas menu overlay, show/hide based on off-canvas menu state.

              Entering: "transition-opacity ease-linear duration-300"
                From: "opacity-0"
                To: "opacity-100"
              Leaving: "transition-opacity ease-linear duration-300"
                From: "opacity-100"
                To: "opacity-0"
            -->
            <div class="fixed inset-0" transition:fade={{duration: 300}}>
                <div class="absolute inset-0 bg-gray-600 opacity-75" on:click={() => mobileActive = false}></div>
            </div>
            <!--
              Off-canvas menu, show/hide based on off-canvas menu state.

              Entering: "transition ease-in-out duration-300 transform"
                From: "-translate-x-full"
                To: "translate-x-0"
              Leaving: "transition ease-in-out duration-300 transform"
                From: "translate-x-0"
                To: "-translate-x-full"
            -->
            <div class="relative flex-1 flex flex-col max-w-xs w-full pt-5 pb-4 bg-white transition ease-in-out duration-300 transform"
                 transition:fly="{{ x: -300, duration: 300 }}">
                <div class="absolute top-0 right-0 -mr-14 p-1">
                    <button class="flex items-center justify-center h-12 w-12 rounded-full focus:outline-none focus:bg-gray-600"
                            aria-label="Close sidebar" on:click={() => mobileActive=false}>
                        <span class="h-6 w-6 text-white"><XIcon size="24"/></span>
                    </button>
                </div>
                <div class="flex-shrink-0 flex items-center px-4">
                    <img class="h-8 w-auto" src={semlytics} alt="Semlytics logo"/>
                </div>
                <div class="mt-5 flex-1 h-0 overflow-y-auto">
                    <div class="px-2 pb-3">
                        <SiteSelector/>
                    </div>
                    <nav class="px-2">
                        {#each items as item}
                            <Link class="mobile-link" href={item.url.replace(":siteId", siteId)}>
                            <span class=" mr-4 h-6 w-6 text-gray-500 group-hover:text-gray-500 group-focus:text-gray-600 transition ease-in-out duration-150">
                                <svelte:component this={item.icon} size="24"/>
                            </span>
                                {item.label}
                            </Link>
                        {/each}
                    </nav>
                </div>
            </div>
            <div class="flex-shrink-0 w-14">
                <!-- Dummy element to force sidebar to shrink to fit close icon -->
            </div>
        </div>
    {/if}
{:else}
    <div class="flex flex-col w-64 border-r border-gray-200 pt-5 pb-4 bg-white">
        <div class="flex items-center flex-shrink-0 px-4">
            <img class="h-8 w-auto" src={semlytics} alt="Semlytics logo"/>
        </div>
        <div class="mt-5 h-0 flex-1 flex flex-col overflow-y-auto">
            <div class="px-2 pb-3">
                <SiteSelector/>
            </div>
            <!-- Sidebar component, swap this element with another sidebar if you like -->
            <nav class="flex-1 px-2 bg-white">
                {#each items as item}
                    <Link href={item.url.replace(":siteId", siteId)} class="link">
                            <span class=" mr-4 h-6 w-6 text-gray-500 group-hover:text-gray-500 group-focus:text-gray-600 transition ease-in-out duration-150">
                                <svelte:component this={item.icon} size="24"/>
                            </span>
                        {item.label}
                    </Link>
                {/each}
            </nav>
        </div>
    </div>
{/if}
