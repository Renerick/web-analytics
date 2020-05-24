<script>
    import {SmileIcon} from "svelte-feather-icons";
    import {Router, Route, Link, router} from "yrv";

    import Header from "./components/Header.svelte";
    import Toolbar from "./components/Toolbar.svelte";
    import Navpanel from "./components/Navpanel.svelte";

    import svelteLogo from "images/svelte-logo.svg";
    import tailwindLogo from "images/tailwindcss-logo.svg";

    // for the sake of simplicity
    // remove if you have SSR and want to use it with the router
    Router.hashchange = true;

    let mobileMenu;

    import Dashboard from "./pages/Dashboard.svelte";
    import SessionList from "./pages/SessionList.svelte";
    import SessionRecording from "./pages/SessionRecording.svelte";
    import PageList from "./pages/PageList.svelte";
    import Heatmap from "./pages/Heatmap.svelte";
    import Redirector from "./components/Redirector.svelte";
</script>

<style>
    :global(body) {
        @apply bg-gray-50;
        overflow: auto;
    }

    :global(.bg-primary) {
        @apply bg-blue-600;
    }

    :global(.text-primary) {
        @apply text-blue-600;
    }
</style>

<svelte:head>
    <link rel="stylesheet" href="https://rsms.me/inter/inter.css">
</svelte:head>

<div class="h-screen flex overflow-hidden bg-gray-100">
    <!-- Off-canvas menu for mobile -->
    <div class="md:hidden">
        <Navpanel bind:this={mobileMenu} mobile={true}/>
    </div>

    <!-- Static sidebar for desktop -->
    <div class="hidden md:flex md:flex-shrink-0">
        <Navpanel/>
    </div>
    <div class="flex flex-col w-0 flex-1 overflow-hidden">
        <div class="md:hidden pl-1 pt-1 sm:pl-3 sm:pt-3">
            <button class="-ml-0.5 -mt-0.5 h-12 w-12 inline-flex items-center justify-center rounded-md text-gray-500 hover:text-gray-900 focus:outline-none focus:bg-gray-200 transition ease-in-out duration-150"
                    on:click={() => mobileMenu.open()}  aria-label="Open sidebar">
                <svg class="h-6 w-6" stroke="currentColor" fill="none" viewBox="0 0 24 24">
                    <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"/>
                </svg>
            </button>
        </div>
        <main class="flex-1 relative z-0 overflow-y-auto py-6 focus:outline-none" tabindex="0">
            <div class="max-w-7xl mx-auto px-4 sm:px-6 md:px-8">
                <Router>
                    <Route exact path="/site/:siteId/dashboard">
                        <Dashboard/>
                    </Route>
                    <Route exact path="/site/:siteId/sessions">
                        <SessionList/>
                    </Route>
                    <Route exact path="/site/:siteId/sessions/:sessionId/recording">
                        <SessionRecording/>
                    </Route>
                    <Route exact path="/site/:siteId/pages">
                        <PageList/>
                    </Route>
                    <Route path="/site/:siteId/pages/:url/heatmap">
                        <Heatmap/>
                    </Route>
                    <Route path="/site/:siteId">
                        <h1 class="text-6xl text-red-600">Not Found!</h1>
                    </Route>
                    <Route exact path="/">
                        <Redirector/>
                    </Route>
                    <Route exact path="/site">
                        <Redirector/>
                    </Route>
                </Router>
            </div>
        </main>
    </div>
</div>
