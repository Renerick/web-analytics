<script>
    import {Router, router, navigateTo} from "yrv";
    import ky from "ky";

    let sites = [];
    let selectedValue = null;
    let initialized = false;

    import {onMount} from 'svelte'

    onMount(async () => {
        let data = await ky.get("http://localhost:5000/api/v1/sites").json();
        sites = data.map(i => ({value: i.siteId, label: i.name}));
        selectedValue = sites.filter(s => s.value === $router.params.siteId)[0];
        initialized = true;
    });
</script>

<label for="email" class="sr-only">Email</label>
<div class="mt-1 relative rounded-md shadow-sm">
    <select id="email" class="form-select block w-full sm:text-sm sm:leading-5" bind:value={selectedValue} on:change={e => initialized && navigateTo($router.path.replace(/\/site\/[\w\d-]+\//, '/site/' + e.detail.value + "/" ))}>
        {#each sites as site, i(site.id)}
            <option value="{site.id}">{site.label}</option>
        {/each}
    </select>
</div>