<script>
    import Select from "svelte-select";

    let items = [
        { value: "sstu.ru", label: "sstu.ru" },
        { value: "rasp.sstu.ru", label: "rasp.sstu.ru" }
    ];

    let selectedValue = items[0];

    let periods = [
        { label: "Сегодня" },
        { label: "Вчера" },
        { label: "Неделя" },
        { label: "Месяц" },
        { label: "3 месяца" },
        { label: "Год" }
    ];
</script>

<style>
    :global(svg) {
        vertical-align: baseline !important;
    }

    .site-selector {
        --border: none;
        --background: none;
        --placeholderColor: black;
        --indicatorColor: black;
    }
</style>

<div class="my-4 w-full site-selector flex justify-between items-baseline">
    <Select
        {items}
        bind:selectedValue
        isSearchable={false}
        isClearable={false} />
    <div>
        <ul class="flex flex-row-reverse">
            {#each periods as period}
                <!-- content here -->
                <li class="mx-2 text-white bg-primary rounded-full px-4 py-2">{period.label}</li>
            {/each}
        </ul>
    </div>
</div>
