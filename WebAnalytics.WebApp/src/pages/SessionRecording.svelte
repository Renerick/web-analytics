<script>
    import SvelteTable from "svelte-table";
    import { onMount } from "svelte";
    import { Link, router } from "yrv";
    import ky from "ky";
    import { PlayIcon } from "svelte-feather-icons";
    import Recjs from "../lib/Recjs";

    let loading = false;
    let frame;

    onMount(async () => {
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
        console.log(recording);
        loading = false;
        setTimeout(() => {
            window.document.domain = "localhost:8082"
            const recjs = new Recjs({
                fps: 30,
                document: frame.contentWindow.document
            });

            recjs.player.play(recording[0].frames, {});
        }, 1000);
    });
</script>

<iframe
    style="position:absolute; width: 100%; height: 100vh"
    title="viewport"
    bind:this={frame}
    src="http://localhost:8082/mcadventure.net/index.html" />
