<script>
    export let count = 1;
    export let forms = [];

    let label = "";

    $: {
        if (forms.length < 3)
            label = "";
        else
            switch (count % 10) {
                case 1:
                    label = forms[0];
                    break;
                case 2:
                case 3:
                case 4:
                    label = forms[1];
                    break;
                case 0:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    label = forms[2];
                    break;
            }
    }
</script>

{count} {label}