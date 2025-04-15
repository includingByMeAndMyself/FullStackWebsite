import React from 'react';
import {Title} from "@/components/shared/title";
import {FilterCheckbox} from "@/components/shared/filter-checkbox";
import {Input} from "@/components/ui";
import {RangeSlider} from "@/components/shared/range-slider";
import {CheckboxFilterGroup} from "@/components/shared/checkbox-filter-group";

interface Props {
    className?: string;
}

export const Filters: React.FC<Props> = ({className}) => {
    return (
        <div className={className}>
            <Title text="Фильтрация" size="sm" className="mb-5 font-bold"/>

            {/*Верхние чекбоксы*/}
            <div className="flex flex-col gap-4">
                <FilterCheckbox text="Можно собирать" value="1"/>
                <FilterCheckbox text="Новинки" value="2"/>
            </div>

            {/*Минимальная/Максимальная цена*/}
            <div className="mt-5 border-y border-y-neutral-100 py-6 pb-7">
                <p className="font-bold mb-3">Цена от и до:</p>
                <div className="flex gap-3 mb-5">
                    <Input type="number" placeholder="0" min={0} max={3000} defaultValue={0} />
                    <Input type="number" min={100} max={3000} placeholder="3000" />
                </div>

                {/*Слайдер*/}
                <RangeSlider min={0} max={3000} step={10} value={[0, 3000]}/>
            </div>

            {/*Список ингридиентов*/}
            <CheckboxFilterGroup
                title="Ингридиенты"
                className='mt-5'
                limit={5}
                defaultItems={[
                    {
                        text: 'Сырный соус',
                        value: '1'
                    },
                    {
                        text: 'Моццарелла',
                        value: '2'
                    },
                    {
                        text: 'Чеснок',
                        value: '3'
                    },
                    {
                        text: 'Солённые огурчики',
                        value: '4'
                    },
                    {
                        text: 'Красный лук',
                        value: '5'
                    },
                    {
                        text: 'Томаты',
                        value: '6'
                    }
                ]}
                items={[
                    {
                        text: 'Сырный соус',
                        value: '1'
                    },
                    {
                        text: 'Моццарелла',
                        value: '2'
                    },
                    {
                        text: 'Чеснок',
                        value: '3'
                    },
                    {
                        text: 'Солённые огурчики',
                        value: '4'
                    },
                    {
                        text: 'Красный лук',
                        value: '5'
                    },
                    {
                        text: 'Томаты',
                        value: '6'
                    }
                ]}
            />
        </div>
    );
};