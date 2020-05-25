<script>
    import BrowserIcon from "../components/BrowserIcon.svelte";
    import OsIcon from "../components/OsIcon.svelte";

    export let session
</script>

<div class="px-4 pt-5 pb-4 sm:p-6  border-b border-gray-200 pt-1">
    <h3 class="text-lg leading-6 font-medium text-gray-900">
        Сессия {session.start}
    </h3>
    <p class="mt-1 max-w-2xl text-sm leading-5 text-gray-500">
        {session.sessionId}
    </p>
</div>
<div class="px-4 pt-5 pb-4 sm:p-6">
    <dl class="grid grid-cols-1 col-gap-4 row-gap-8 sm:grid-cols-2">
        <div class="sm:col-span-2">
            <dt class="text-sm leading-5 font-medium text-gray-500">
                Идентификатор посетителя
            </dt>
            <dd class="mt-1 text-sm leading-5 text-gray-900">
                {session.visitorId}
            </dd>
        </div>
        <div class="sm:col-span-2">
            <dt class="text-sm leading-5 font-medium text-gray-500">
                User agent
            </dt>
            <dd class="mt-1 text-sm leading-5 text-gray-900">
                {session.deviceInfo.userAgent}
            </dd>
        </div>
        <div class="sm:col-span-1">
            <dt class="text-sm leading-5 font-medium text-gray-500">
                Операционная система
            </dt>
            <dd class=" mt-1 flex-1 flex items-center text-sm leading-5 text-gray-900">
                <OsIcon class="flex-shrink-0 h-5 w-5 mr-2 text-gray-400" size="24" systemType="{session.deviceInfo.operatingSystem.type}"/>
                {session.deviceInfo.operatingSystem.name}
            </dd>
        </div>
        <div class="sm:col-span-1">
            <dt class="text-sm leading-5 font-medium text-gray-500">
                Браузер
            </dt>
            <dd class=" mt-1 flex-1 flex items-center text-sm leading-5 text-gray-900">
                <BrowserIcon class="flex-shrink-0 h-5 w-5 mr-2 text-gray-400" size="24" browserType="{session.deviceInfo.browser.type}"/>
                {session.deviceInfo.browser.name}
            </dd>
        </div>
        <div class="sm:col-span-2">
            <dt class="text-sm leading-5 font-medium text-gray-500">
                Посещенные страницы
            </dt>
            <dd class="mt-1 text-sm leading-5 text-gray-900">
                <div class="bg-white overflow-hidden sm:rounded-md">
                    <ul>
                        {#each session.recordingFragments as fragment, i(fragment.fragmentId)}
                            <li>
                                <div class="block hover:bg-gray-50 focus:outline-none focus:bg-gray-50 transition duration-150 ease-in-out">
                                    <div class="py-2 ">
                                        <div class="flex items-baseline justify-between">
                                            <div class="text-sm leading-5 font-medium text-blue-600 truncate">
                                                {fragment.url}
                                            </div>
                                            <div class="mt-2 flex items-center text-sm leading-5 text-gray-500 sm:mt-0">
                                                <span>
                                                    {new Date(fragment.frames.frames.length/fragment.frames.fps * 1000).toISOString().substr(11, 8)}
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        {/each}
                    </ul>
                </div>
            </dd>
        </div>
    </dl>
</div>