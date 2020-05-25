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

    function setHeight() {
        let doc = frame.contentWindow.document;
        doc.body.style.position = 'relative';
        let overlay = doc.createElement('div');
        overlay.style.position = "absolute";
        overlay.style.zIndex = 999;
        overlay.style.top = 0;
        overlay.style.height = "100%";
        overlay.style.display = "flex";
        overlay.style.flexDirection = "column";
        overlay.innerHTML = "<img style='opacity: 0.75' src='" + "http:\/\/localhost:5000/api/v1/site/" + $router.params.siteId + "/pages/" + encodeURIComponent($router.params.url) + "/heatmap" + "' /><div style='flex-grow: 1;width: 100%;background: rgba(0,0,0,0.75)'></div>";

        doc.body.appendChild(overlay);
    }

</script>

<iframe src={"http://localhost:5000/proxy/" +$router.params.url}
        class="flex-grow"
        style="min-width: 1800px; width: 100%; height: 100%; overflow: visible"
        title="viewport"
        on:load={()    =>    setHeight()    }
        bind:this= {frame} ></iframe>
