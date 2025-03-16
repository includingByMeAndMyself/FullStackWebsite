import {Container, Filters, ProductsGroupList, Title, TopBar} from "@/components/shared";

export default function Home() {
  return (<>
          <Container className="mt-10">
              <Title text="Все пиццы" size="lg" className="font-extrabold"/>
          </Container>

          <TopBar/>

          <Container className="mt-10 pb-14">
              <div className="flex gap-[60px]">
                  {/*Фильтрация*/}
                  <div className="w-[250px]">
                      <Filters/>
                  </div>

                  {/*Список товаров*/}
                  <div className="flex-1">
                      <div className="flex flex-col gap-16">
                          <ProductsGroupList title="Пиццы"
                                             items={[
                                                 {
                                                     id:1,
                                                     name:'Чизбургер пицца',
                                                     imageUrl: 'https://media.dodostatic.net/image/r:292x292/11ee7d61698827ee9b8db6d0aec53410.avif',
                                                     price:550,
                                                     items: [{price:550}]
                                                 },
                                                 {
                                                     id:2,
                                                     name:'Чизбургер пицца',
                                                     imageUrl: 'https://media.dodostatic.net/image/r:292x292/11ee7d61698827ee9b8db6d0aec53410.avif',
                                                     price:550,
                                                     items: [{price:550}]
                                                 },
                                                 {
                                                     id:3,
                                                     name:'Чизбургер пицца',
                                                     imageUrl: 'https://media.dodostatic.net/image/r:292x292/11ee7d61698827ee9b8db6d0aec53410.avif',
                                                     price:550,
                                                     items: [{price:550}]
                                                 },
                                                 {
                                                     id:4,
                                                     name:'Чизбургер пицца',
                                                     imageUrl: 'https://media.dodostatic.net/image/r:292x292/11ee7d61698827ee9b8db6d0aec53410.avif',
                                                     price:550,
                                                     items: [{price:550}]
                                                 },
                                                 {
                                                     id:5,
                                                     name:'Чизбургер пицца',
                                                     imageUrl: 'https://media.dodostatic.net/image/r:292x292/11ee7d61698827ee9b8db6d0aec53410.avif',
                                                     price:550,
                                                     items: [{price:550}]
                                                 },
                                                 {
                                                     id:6,
                                                     name:'Чизбургер пицца',
                                                     imageUrl: 'https://media.dodostatic.net/image/r:292x292/11ee7d61698827ee9b8db6d0aec53410.avif',
                                                     price:550,
                                                     items: [{price:550}]
                                                 },
                                             ]}
                                             categoryId={1}/>
                      </div>
                  </div>
              </div>
          </Container>
      </>
  );
}
