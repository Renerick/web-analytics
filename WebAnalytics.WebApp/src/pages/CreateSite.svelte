<!-- Be sure to use this with a layout container that is full-width on mobile -->
<div class="bg-white overflow-hidden shadow sm:rounded-lg sm:my-6">
    <div class="px-4 py-5 sm:p-6">
        <form>
            <div>
                <div>
                    <div>
                        <h3 class="text-lg leading-6 font-medium text-gray-900">
                            Сайт
                        </h3>
                    </div>
                    <div class="mt-6 grid grid-cols-1 row-gap-6 col-gap-4 sm:grid-cols-6">
                        <div class="sm:col-span-6">
                            <label for="username" class="block text-sm font-medium leading-5 text-gray-700">
                                Домен
                            </label>
                            <div class="mt-1 flex rounded-md shadow-sm w-full">
            <span class="inline-flex items-center px-3 rounded-l-md border border-r-0 border-gray-300 bg-gray-50 text-gray-500 sm:text-sm">
              http://
            </span>
                                <input id="username"
                                       class="flex-1 form-input block w-full rounded-none rounded-r-md transition duration-150 ease-in-out sm:text-sm sm:leading-5"/>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="mt-8 border-t border-gray-200 pt-5">
                <div class="flex justify-end">
      <span class="inline-flex rounded-md shadow-sm">
        <button type="button"
                class="py-2 px-4 border border-gray-300 rounded-md text-sm leading-5 font-medium text-gray-700 hover:text-gray-500 focus:outline-none focus:border-blue-300 focus:shadow-outline-blue active:bg-gray-50 active:text-gray-800 transition duration-150 ease-in-out">
          Отмена
        </button>
      </span>
                    <span class="ml-3 inline-flex rounded-md shadow-sm">
        <button type="submit"
                on:submit|preventDefault|stopPropagation={() => ky}
                class="inline-flex justify-center py-2 px-4 border border-transparent text-sm leading-5 font-medium rounded-md text-white bg-blue-600 hover:bg-indigo-500 focus:outline-none focus:border-indigo-700 focus:shadow-outline-indigo active:bg-indigo-700 transition duration-150 ease-in-out">
          Добавить
        </button>
      </span>
                </div>
            </div>
        </form>
    </div>
</div>

