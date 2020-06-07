<script>
    export let sessionCount;
    export let averageDepth;
    export let uniqueVisitors;
</script>

<h3 class="text-lg leading-6 font-medium text-gray-900">
    Список сессий
</h3>
<div class="mt-5 grid grid-cols-1 rounded-lg bg-white overflow-hidden shadow md:grid-cols-3">
    <div>
        <div class="px-4 py-5 sm:p-6">
            <dl>
                <dt class="text-base leading-6 font-normal text-gray-900">
                    Всего сессий
                </dt>
                <dd class="mt-1 flex justify-between items-baseline md:block lg:flex">
                    <div class="flex items-baseline text-2xl leading-8 font-semibold text-blue-600">
                        {sessionCount}
                    </div>
                </dd>
            </dl>
        </div>
    </div>
    <div class="border-t border-gray-200 md:border-0 md:border-l">
        <div class="px-4 py-5 sm:p-6">
            <dl>
                <dt class="text-base leading-6 font-normal text-gray-900">
                    Средняя глубина просмотра
                </dt>
                <dd class="mt-1 flex justify-between items-baseline md:block lg:flex">
                    <div class="flex items-baseline text-2xl leading-8 font-semibold text-blue-600">
                        {averageDepth}
                    </div>
                </dd>
            </dl>
        </div>
    </div>
    <div class="border-t border-gray-200 md:border-0 md:border-l">
        <div class="px-4 py-5 sm:p-6">
            <dl>
                <dt class="text-base leading-6 font-normal text-gray-900">
                    Уникальных посетителей
                </dt>
                <dd class="mt-1 flex justify-between items-baseline md:block lg:flex">
                    <div class="flex items-baseline text-2xl leading-8 font-semibold text-blue-600">
                        {uniqueVisitors}
                    </div>
                </dd>
            </dl>
        </div>
    </div>
</div>
