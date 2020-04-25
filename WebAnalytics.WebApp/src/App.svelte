<script>
    import Header from "./Header.svelte";
    import { Router, Route, Link, router, navigateTo } from "yrv";

    // for the sake of simplicity
    // remove if you have SSR and want to use it with the router
    Router.hashchange = true;

    let sites = [
        { value: "sstu.ru", label: "Sstu.ru" },
        { value: "rasp.sstu.ru", label: "rasp.sstu.ru" }
    ];

    let site = "sstu.ru";

    function setSite() {
        console.log(site);
        console.log($router);
        router.update(s => s.params = {site: site});
    }
</script>

<style>
    :global(body) {
        @apply bg-gray-100;
    }
</style>

<div class="w-full">
    <Header />

    <Router path="/:site">
        <Route>
            <div>
                <div id="site-selector" class="flex">
                    <select
                        name="site"
                        id="site-select"
                        bind:value={site}
                        on:change={setSite}>
                        {#each sites as site}
                            <option value={site.value}>{site.label}</option>
                        {/each}
                    </select>
                </div>
                <Router>
                    <Route path="/hello">
                        <h1 class="text-6xl">Hello world!</h1>
                    </Route>
                    <Route fallback>
                        <h1 class="text-6xl text-red-600">Not Found!</h1>
                    </Route>
                </Router>
            </div>
        </Route>
    </Router>
</div>
