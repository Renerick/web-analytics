<script>
    import {FrownIcon} from 'svelte-feather-icons';
    import Loader from "./Loader.svelte";

    export let error = false;
    export let loading = false;
    export let func;

    $: (async () => {
        loading = true;
        try {
            await func();
        } catch (e) {
            error = true;
        }
        loading = false;
    })();
</script>

{#if error}
    <div class="h-64 text-gray-400 flex flex-1 flex-col justify-center items-center">
        <FrownIcon size="48"/>
        <div class="text-medium text-lg">
            Произошла ошибка
        </div>
    </div>
{:else if loading}
    <div class="h-64 text-gray-400  flex flex-1 flex-col  justify-center items-center ">
        <Loader/>
    </div>
{:else}
    <slot/>
{/if}