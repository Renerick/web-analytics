<script>
    import Select from "svelte-select";
    import { onMount } from "svelte";
    import { Router, Route, Link, router, navigateTo } from "yrv";
    import semlytics from "images/semlytics-logo-color-small.svg";
    import ky from "ky";

    let sites = [];
    let selectedValue = null;
    let initialized = false;

    onMount(async () => {
        let data = await ky.get("http://localhost:5000/api/v1/sites").json();
        sites = data.map(i => ({ value: i.siteId, label: i.name }));
        selectedValue = sites.filter(s => s.value === $router.params.siteId)[0];
        initialized = true;
    });
</script>

<style>
    .select-custom {
        --height: 37px;
    }
</style>

<div class="w-full self-center">
    <div class="flex justify-between items-stretch my-4">
        <div>
            <img src={semlytics} alt="Semlytics logo" />
        </div>
        <div class="ml-auto w-64 select-custom">
            <Select
                bind:items={sites}
                bind:selectedValue
                isSearchable={false}
                isClearable={false}
                on:select={e => initialized && navigateTo($router.path.replace(/\/site\/[\w\d-]+\//, '/site/' + e.detail.value + "/" ))} />
        </div>
        <div class="ml-4">
            <div style="width:37px;height:37px" class="bg-primary rounded" />
        </div>
    </div>
</div>
