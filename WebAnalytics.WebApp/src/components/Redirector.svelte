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

        console.log($router);

        selectedValue = sites[0];

        if (sites.length > 0)
            navigateTo("/site/" + selectedValue.value + "/sessions", {
                replace: true
            });
    });
</script>
