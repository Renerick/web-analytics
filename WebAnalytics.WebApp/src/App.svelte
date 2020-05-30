<script>
    import {SmileIcon} from "svelte-feather-icons";
    import Modal from 'svelte-simple-modal';
    import {Router, Route, Link, router} from "yrv";

    // for the sake of simplicity
    // remove if you have SSR and want to use it with the router
    Router.hashchange = true;
    import Dashboard from "./pages/Dashboard.svelte";
    import SessionList from "./pages/SessionList.svelte";
    import SessionRecording from "./pages/SessionRecording.svelte";
    import PageList from "./pages/PageList.svelte";
    import Heatmap from "./pages/Heatmap.svelte";
    import Redirector from "./components/Redirector.svelte";
    import SiteSettings from "./pages/SiteSettings.svelte";
    import SidePanel from "./layouts/SidePanel.svelte";
    import TopBarWIthBackButton from "./layouts/TopBarWIthBackButton.svelte";
    import CreateSite from "./pages/CreateSite.svelte";
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

<Router>
    <Route exact path="/site/:siteId/dashboard">
        <SidePanel>
            <Dashboard/>
        </SidePanel>
    </Route>
    <Route exact path="/addSite">
        <TopBarWIthBackButton backUrl="/">
            <CreateSite/>
        </TopBarWIthBackButton>
    </Route>
    <Route exact path="/site/:siteId/sessions">
        <SidePanel>
            <SessionList/>
        </SidePanel>
    </Route>
    <Route exact path="/site/:siteId/sessions/:sessionId/recording" >
        <TopBarWIthBackButton backUrl={"/site/:siteId/sessions".replace(":siteId", $router.params.siteId).replace()}>
            <SessionRecording/>
        </TopBarWIthBackButton>
    </Route>
    <Route exact path="/site/:siteId/pages">
        <SidePanel>
            <PageList/>
        </SidePanel>
    </Route>
    <Route exact path="/site/:siteId/settings">
        <SidePanel>
            <SiteSettings/>
        </SidePanel>
    </Route>
    <Route path="/site/:siteId/pages/:url/heatmap">
        <TopBarWIthBackButton backUrl={"/site/:siteId/pages".replace(":siteId", $router.params.siteId).replace()}>
            <Heatmap/>
        </TopBarWIthBackButton>
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
