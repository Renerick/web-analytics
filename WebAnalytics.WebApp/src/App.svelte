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

    import Dashboard from "./pages/Dashboard.svelte";
    import SessionList from "./pages/SessionList.svelte";
    import SessionRecording from "./pages/SessionRecording.svelte";
    import PageList from "./pages/PageList.svelte";
    import Heatmap from "./pages/Heatmap.svelte";
    import Redirector from "./components/Redirector.svelte";
</script>

<style>
    :global(body) {
        @apply bg-gray-100;
        overflow: auto;
    }

    :global(.bg-primary) {
        @apply bg-blue-600;
    }
    :global(.text-primary) {
        @apply text-blue-700;
    }
</style>

<div class="w-full items-center">
    <Header />
    <div class="flex">
        <aside class="w-48 flex-shrink-0">
            <Navpanel />
        </aside>
        <main class="flex flex-grow flex-col px-4">
            <Router>
                <Route exact path="/site/:siteId/dashboard">
                    <Dashboard />
                </Route>
                <Route exact path="/site/:siteId/sessions">
                    <SessionList />
                </Route>
                <Route exact path="/site/:siteId/sessions/:sessionId/recording">
                    <SessionRecording />
                </Route>
                <Route exact path="/site/:siteId/pages">
                    <PageList />
                </Route>
                <Route path="/site/:siteId/pages/heatmap/*url">
                    <Heatmap />
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
        </main>
    </div>
</div>
