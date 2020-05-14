<script>
    import { SmileIcon } from "svelte-feather-icons";
    import { Router, Route, Link, router } from "yrv";

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
</style>

<div class="w-full items-center container mx-auto">
    <Header />
    <div class="flex">
        <aside class="w-1/6">
            <Navpanel />
        </aside>
        <main class="w-5/6 flex flex-col pl-4">
            <Router>
                <Route exact path="/site/:siteId/dashboard">
                    <Dashboard />
                </Route>
                <Route exact path="/site/:siteId/sessions">
                    <SessionList />
                </Route>
                <Route exact path="/site/:siteId/session/:sessionId/recording">
                    <SessionRecording />
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
                <!-- <Route fallback>
                    <h1 class="text-6xl text-red-600">Not Found!</h1>
                </Route> -->
            </Router>
        </main>
    </div>
</div>
