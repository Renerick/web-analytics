<script>
    import {router} from 'yrv';

    let label = "<script src='" + window.location.origin +"/recorder.js'><\/script><script>const recjs = new Tracker({fps: 30, site: '" + $router.params.siteId + "', endpoint: '" + window.location.origin + "' })" + "<\/script>";
</script>

<div class="bg-white shadow px-4 py-5 sm:rounded-lg sm:p-6">
    <div class="md:grid md:grid-cols-3 md:gap-6">
        <div class="md:col-span-1">
            <h3 class="text-lg font-medium leading-6 text-gray-900">Сниппет</h3>
            <p class="mt-1 text-sm leading-5 text-gray-500">
                Скопируйте этот код на страницы, на которых хотите обеспечить сбор данных
            </p>
        </div>
        <div class="mt-5 md:mt-0 md:col-span-2">
            <div>
                <label class="sr-only" for="snippet">Код счетчика</label>
                    <textarea rows="5" class="form-textarea block w-full transition duration-150 ease-in-out sm:text-sm sm:leading-5 resize-none font-mono" id="snippet" bind:value={label} readonly></textarea>
            </div>
        </div>
    </div>
</div>

<!--<div class="mt-6 bg-white shadow px-4 py-5 sm:rounded-lg sm:p-6">-->
<!--    <div class="md:grid md:grid-cols-3 md:gap-6">-->
<!--        <div class="md:col-span-1">-->
<!--            <h3 class="text-lg font-medium leading-6 text-gray-900">Personal Information</h3>-->
<!--            <p class="mt-1 text-sm leading-5 text-gray-500">-->
<!--                Use a permanent address where you can receive mail.-->
<!--            </p>-->
<!--        </div>-->
<!--        <div class="mt-5 md:mt-0 md:col-span-2">-->
<!--            <form action="#" method="POST">-->
<!--                <div class="grid grid-cols-6 gap-6">-->
<!--                    <div class="col-span-6 sm:col-span-3">-->
<!--                        <label for="first_name" class="block text-sm font-medium leading-5 text-gray-700">First-->
<!--                            name</label>-->
<!--                        <input id="first_name"-->
<!--                               class="mt-1 form-input block w-full py-2 px-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:shadow-outline-blue focus:border-blue-300 transition duration-150 ease-in-out sm:text-sm sm:leading-5"/>-->
<!--                    </div>-->

<!--                    <div class="col-span-6 sm:col-span-3">-->
<!--                        <label for="last_name" class="block text-sm font-medium leading-5 text-gray-700">Last-->
<!--                            name</label>-->
<!--                        <input id="last_name"-->
<!--                               class="mt-1 form-input block w-full py-2 px-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:shadow-outline-blue focus:border-blue-300 transition duration-150 ease-in-out sm:text-sm sm:leading-5"/>-->
<!--                    </div>-->

<!--                    <div class="col-span-6 sm:col-span-4">-->
<!--                        <label for="email_address" class="block text-sm font-medium leading-5 text-gray-700">Email-->
<!--                            address</label>-->
<!--                        <input id="email_address"-->
<!--                               class="mt-1 form-input block w-full py-2 px-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:shadow-outline-blue focus:border-blue-300 transition duration-150 ease-in-out sm:text-sm sm:leading-5"/>-->
<!--                    </div>-->

<!--                    <div class="col-span-6 sm:col-span-3">-->
<!--                        <label for="country" class="block text-sm font-medium leading-5 text-gray-700">Country /-->
<!--                            Region</label>-->
<!--                        <select id="country"-->
<!--                                class="mt-1 block form-select w-full py-2 px-3 py-0 border border-gray-300 bg-white rounded-md shadow-sm focus:outline-none focus:shadow-outline-blue focus:border-blue-300 transition duration-150 ease-in-out sm:text-sm sm:leading-5">-->
<!--                            <option>United States</option>-->
<!--                            <option>Canada</option>-->
<!--                            <option>Mexico</option>-->
<!--                        </select>-->
<!--                    </div>-->

<!--                    <div class="col-span-6">-->
<!--                        <label for="street_address" class="block text-sm font-medium leading-5 text-gray-700">Street-->
<!--                            address</label>-->
<!--                        <input id="street_address"-->
<!--                               class="mt-1 form-input block w-full py-2 px-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:shadow-outline-blue focus:border-blue-300 transition duration-150 ease-in-out sm:text-sm sm:leading-5"/>-->
<!--                    </div>-->

<!--                    <div class="col-span-6 sm:col-span-6 lg:col-span-2">-->
<!--                        <label for="city" class="block text-sm font-medium leading-5 text-gray-700">City</label>-->
<!--                        <input id="city"-->
<!--                               class="mt-1 form-input block w-full py-2 px-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:shadow-outline-blue focus:border-blue-300 transition duration-150 ease-in-out sm:text-sm sm:leading-5"/>-->
<!--                    </div>-->

<!--                    <div class="col-span-6 sm:col-span-3 lg:col-span-2">-->
<!--                        <label for="state" class="block text-sm font-medium leading-5 text-gray-700">State /-->
<!--                            Province</label>-->
<!--                        <input id="state"-->
<!--                               class="mt-1 form-input block w-full py-2 px-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:shadow-outline-blue focus:border-blue-300 transition duration-150 ease-in-out sm:text-sm sm:leading-5"/>-->
<!--                    </div>-->

<!--                    <div class="col-span-6 sm:col-span-3 lg:col-span-2">-->
<!--                        <label for="postal_code" class="block text-sm font-medium leading-5 text-gray-700">ZIP /-->
<!--                            Postal</label>-->
<!--                        <input id="postal_code"-->
<!--                               class="mt-1 form-input block w-full py-2 px-3 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:shadow-outline-blue focus:border-blue-300 transition duration-150 ease-in-out sm:text-sm sm:leading-5"/>-->
<!--                    </div>-->
<!--                </div>-->
<!--            </form>-->
<!--        </div>-->
<!--    </div>-->
<!--</div>-->

<!--<div class="mt-6 bg-white shadow px-4 py-5 sm:rounded-lg sm:p-6">-->
<!--    <div class="md:grid md:grid-cols-3 md:gap-6">-->
<!--        <div class="md:col-span-1">-->
<!--            <h3 class="text-lg font-medium leading-6 text-gray-900">Notifications</h3>-->
<!--            <p class="mt-1 text-sm leading-5 text-gray-500">-->
<!--                Decide which communications you'd like to receive and how.-->
<!--            </p>-->
<!--        </div>-->
<!--        <div class="mt-5 md:mt-0 md:col-span-2">-->
<!--            <form action="#" method="POST">-->
<!--                <fieldset>-->
<!--                    <legend class="text-base leading-6 font-medium text-gray-900">By Email</legend>-->
<!--                    <div class="mt-4">-->
<!--                        <div class="flex items-start">-->
<!--                            <div class="absolute flex items-center h-5">-->
<!--                                <input id="comments" type="checkbox"-->
<!--                                       class="form-checkbox h-4 w-4 text-indigo-600 transition duration-150 ease-in-out"/>-->
<!--                            </div>-->
<!--                            <div class="pl-7 text-sm leading-5">-->
<!--                                <label for="comments" class="font-medium text-gray-700">Comments</label>-->
<!--                                <p class="text-gray-500">Get notified when someones posts a comment on a posting.</p>-->
<!--                            </div>-->
<!--                        </div>-->
<!--                        <div class="mt-4">-->
<!--                            <div class="flex items-start">-->
<!--                                <div class="absolute flex items-center h-5">-->
<!--                                    <input id="candidates" type="checkbox"-->
<!--                                           class="form-checkbox h-4 w-4 text-indigo-600 transition duration-150 ease-in-out"/>-->
<!--                                </div>-->
<!--                                <div class="pl-7 text-sm leading-5">-->
<!--                                    <label for="candidates" class="font-medium text-gray-700">Candidates</label>-->
<!--                                    <p class="text-gray-500">Get notified when a candidate applies for a job.</p>-->
<!--                                </div>-->
<!--                            </div>-->
<!--                        </div>-->
<!--                        <div class="mt-4">-->
<!--                            <div class="flex items-start">-->
<!--                                <div class="absolute flex items-center h-5">-->
<!--                                    <input id="offers" type="checkbox"-->
<!--                                           class="form-checkbox h-4 w-4 text-indigo-600 transition duration-150 ease-in-out"/>-->
<!--                                </div>-->
<!--                                <div class="pl-7 text-sm leading-5">-->
<!--                                    <label for="offers" class="font-medium text-gray-700">Offers</label>-->
<!--                                    <p class="text-gray-500">Get notified when a candidate accepts or rejects an-->
<!--                                        offer.</p>-->
<!--                                </div>-->
<!--                            </div>-->
<!--                        </div>-->
<!--                    </div>-->
<!--                </fieldset>-->
<!--                <fieldset class="mt-6">-->
<!--                    <legend class="text-base leading-6 font-medium text-gray-900">Push Notifications</legend>-->
<!--                    <p class="text-sm leading-5 text-gray-500">These are delivered via SMS to your mobile phone.</p>-->
<!--                    <div class="mt-4">-->
<!--                        <div class="flex items-center">-->
<!--                            <input id="push_everything" name="form-input push_notifications" type="radio"-->
<!--                                   class="form-radio h-4 w-4 text-indigo-600 transition duration-150 ease-in-out"/>-->
<!--                            <label for="push_everything" class="ml-3">-->
<!--                                <span class="block text-sm leading-5 font-medium text-gray-700">Everything</span>-->
<!--                            </label>-->
<!--                        </div>-->
<!--                        <div class="mt-4 flex items-center">-->
<!--                            <input id="push_email" name="form-input push_notifications" type="radio"-->
<!--                                   class="form-radio h-4 w-4 text-indigo-600 transition duration-150 ease-in-out"/>-->
<!--                            <label for="push_email" class="ml-3">-->
<!--                                <span class="block text-sm leading-5 font-medium text-gray-700">Same as email</span>-->
<!--                            </label>-->
<!--                        </div>-->
<!--                        <div class="mt-4 flex items-center">-->
<!--                            <input id="push_nothing" name="form-input push_notifications" type="radio"-->
<!--                                   class="form-radio h-4 w-4 text-indigo-600 transition duration-150 ease-in-out"/>-->
<!--                            <label for="push_nothing" class="ml-3">-->
<!--                                <span class="block text-sm leading-5 font-medium text-gray-700">No push notifications</span>-->
<!--                            </label>-->
<!--                        </div>-->
<!--                    </div>-->
<!--                </fieldset>-->
<!--            </form>-->
<!--        </div>-->
<!--    </div>-->
<!--</div>-->

<div class="my-6 bg-white shadow sm:rounded-lg px-4 py-5 sm:p-6">
    <h3 class="text-lg leading-6 font-medium text-gray-900">
        Удалить сайт
    </h3>
    <div class="mt-2 max-w-xl text-sm leading-5 text-gray-500">
        <p>
            Удалив сайт, вы потеряете доступ ко всем его данным!
        </p>
    </div>
    <div class="mt-5">
        <button type="button"
                class="inline-flex items-center justify-center px-4 py-2 border border-transparent font-medium rounded-md text-red-700 bg-red-100 hover:bg-red-50 focus:outline-none focus:border-red-300 focus:shadow-outline-red active:bg-red-200 transition ease-in-out duration-150 sm:text-sm sm:leading-5">
            Удалить
        </button>
    </div>
</div>