import React from 'react';

export class Toolbar extends React.Component {
    constructor(props) {
        super(props);

    }


    render() {
        return (
            <div class="my-4 w-full site-selector flex justify-between items-baseline">
                {/*<Select*/}
                {/*    {items}*/}
                {/*    bind:selectedValue*/}
                {/*    isSearchable={false}*/}
                {/*    isClearable={false}/>*/}
                <div>
                    <ul class="flex flex-row-reverse">
                        <li class="mx-2 text-white bg-primary rounded-full px-4 py-2"></li>
                    </ul>
                </div>
            </div>)
    }
}

