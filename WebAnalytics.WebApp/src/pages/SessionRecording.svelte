<script>
    import SvelteTable from "svelte-table";
    import {onMount, onDestroy} from "svelte";
    import {Link, router} from "yrv";
    import ky from "ky";
    import {PlayIcon} from "svelte-feather-icons";
    import Recjs from "../lib/Recjs";

    let loading = false;
    let frame;

    let recjs;

    onMount(async () => {
        recjs = new Recjs({
            fps: 30,
            frame: frame
        });
        loading = true;
        let recording = await ky
                .get(
                        "http://localhost:5000/api/v1/site/" +
                        $router.params.siteId +
                        "/session/" +
                        $router.params.sessionId +
                        "/recording"
                )
                .json();
        loading = false;


        recjs.player.play(recording, {});
    });

    onDestroy(() => {
        recjs.player.stop();
    })
</script>

<iframe
        style="position:absolute; width: 100%; height: 100vh"
        title="viewport"
        bind:this={frame}></iframe>
