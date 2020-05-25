<script>
    import {Link, router} from "yrv";
    import {PlayIcon, ChevronLeftIcon} from "svelte-feather-icons";

    export let backUrl = "/"
</script>

<div class="flex flex-col absulute w-full h-screen bg-gray-100">
    <nav class="bg-white shadow-sm border-b">
        <div class="w-full mx-auto px-4 sm:px-6 lg:px-8">
            <div class="flex justify-between h-16">
                <div class="flex">
                    <div class="flex-shrink-0 flex items-center px-4">
                        <Link href="{backUrl}">
                            <ChevronLeftIcon size="24"/>
                        </Link>
                    </div>
                </div>
            </div>
        </div>

        <!--
          Mobile menu, toggle classes based on menu state.

          Open: "block", closed: "hidden"
        -->
    </nav>

    <div class="flex-grow">
        <main class="flex flex-col min-h-full relative">
            <div class="flex-grow h-full w-full mx-auto flex flex-col items-center justify-center">
                <slot/>
            </div>
        </main>
    </div>
</div>