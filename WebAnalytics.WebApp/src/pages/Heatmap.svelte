<script>
    import SvelteTable from "svelte-table";
    import {onMount} from "svelte";
    import {Link, router} from "yrv";
    import ky from "ky";
    import {PlayIcon} from "svelte-feather-icons";
    import Recjs from "../lib/Recjs";

    let loading = false;
    let frame;
    let heatmap;

    onMount(async () => {
        loading = true;
        console.log($router);
        // heatmap = await ky
        //         .get(
        //                 "http://localhost:5000/api/v1/site/" +
        //                 $router.params.siteId +
        //                 "/pages/" +
        //                 encodeURIComponent($router.params.url) + "/heatmap"
        //         )
        //         .text();
        loading = false;
    });
</script>

<img src={"http://localhost:5000/api/v1/site/" +
$router.params.siteId +
"/pages/" +
encodeURIComponent($router.params.url) + "/heatmap"} alt="heatmap image">
<iframe
        style="position:absolute; width: 100%; height: 100vh"
        title="viewport"
        bind:this={frame}></iframe>
