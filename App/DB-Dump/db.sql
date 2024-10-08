PGDMP  $    9                |         
   onlineShop    17rc1    17rc1 $    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    16388 
   onlineShop    DATABASE     �   CREATE DATABASE "onlineShop" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "onlineShop";
                     postgres    false            �            1259    16417    order-product    TABLE     �   CREATE TABLE public."order-product" (
    order_id integer NOT NULL,
    product_id integer NOT NULL,
    price double precision DEFAULT 0 NOT NULL,
    quantity integer DEFAULT 0 NOT NULL
);
 #   DROP TABLE public."order-product";
       public         heap r       postgres    false            �           0    0    TABLE "order-product"    ACL     N   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public."order-product" TO manager;
          public               postgres    false    220            �            1259    16403    orders    TABLE     �   CREATE TABLE public.orders (
    id integer NOT NULL,
    user_id uuid NOT NULL,
    sum double precision DEFAULT 0 NOT NULL,
    cheque text,
    created date
);
    DROP TABLE public.orders;
       public         heap r       postgres    false            �           0    0    TABLE orders    ACL     E   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.orders TO manager;
          public               postgres    false    218            �            1259    16391    products    TABLE     �   CREATE TABLE public.products (
    name text NOT NULL,
    description text,
    price double precision NOT NULL,
    category text,
    update_date date,
    id integer NOT NULL
);
    DROP TABLE public.products;
       public         heap r       postgres    false            �           0    0    TABLE products    ACL     G   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.products TO manager;
          public               postgres    false    217            �            1259    16452    products_id_seq    SEQUENCE     �   ALTER TABLE public.products ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.products_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public               postgres    false    217            �            1259    16410    users    TABLE     �   CREATE TABLE public.users (
    id uuid NOT NULL,
    email character varying,
    phone_number character varying,
    password_hash character varying,
    password_salt character varying,
    full_name character varying,
    balance double precision
);
    DROP TABLE public.users;
       public         heap r       postgres    false            �           0    0    TABLE users    ACL     D   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.users TO manager;
          public               postgres    false    219            �            1259    16463    warehouse-product    TABLE     �   CREATE TABLE public."warehouse-product" (
    warehouse_id integer NOT NULL,
    product_id integer NOT NULL,
    delivery_date character varying,
    quantity integer
);
 '   DROP TABLE public."warehouse-product";
       public         heap r       postgres    false            �           0    0    TABLE "warehouse-product"    ACL     R   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public."warehouse-product" TO manager;
          public               postgres    false    223            �            1259    16458 
   warehouses    TABLE     v   CREATE TABLE public.warehouses (
    id integer NOT NULL,
    name character varying,
    adress character varying
);
    DROP TABLE public.warehouses;
       public         heap r       postgres    false            �           0    0    TABLE warehouses    ACL     I   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.warehouses TO manager;
          public               postgres    false    222            �          0    16417    order-product 
   TABLE DATA           P   COPY public."order-product" (order_id, product_id, price, quantity) FROM stdin;
    public               postgres    false    220   �(       �          0    16403    orders 
   TABLE DATA           C   COPY public.orders (id, user_id, sum, cheque, created) FROM stdin;
    public               postgres    false    218   C)       �          0    16391    products 
   TABLE DATA           W   COPY public.products (name, description, price, category, update_date, id) FROM stdin;
    public               postgres    false    217   �)       �          0    16410    users 
   TABLE DATA           j   COPY public.users (id, email, phone_number, password_hash, password_salt, full_name, balance) FROM stdin;
    public               postgres    false    219   !*       �          0    16463    warehouse-product 
   TABLE DATA           `   COPY public."warehouse-product" (warehouse_id, product_id, delivery_date, quantity) FROM stdin;
    public               postgres    false    223   �*       �          0    16458 
   warehouses 
   TABLE DATA           6   COPY public.warehouses (id, name, adress) FROM stdin;
    public               postgres    false    222   �*       �           0    0    products_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('public.products_id_seq', 52, true);
          public               postgres    false    221            <           2606    16421     order-product order-product_pkey 
   CONSTRAINT     t   ALTER TABLE ONLY public."order-product"
    ADD CONSTRAINT "order-product_pkey" PRIMARY KEY (order_id, product_id);
 N   ALTER TABLE ONLY public."order-product" DROP CONSTRAINT "order-product_pkey";
       public                 postgres    false    220    220            6           2606    16428    orders order_pkey 
   CONSTRAINT     O   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT order_pkey PRIMARY KEY (id);
 ;   ALTER TABLE ONLY public.orders DROP CONSTRAINT order_pkey;
       public                 postgres    false    218            8           2606    16447    orders pkey 
   CONSTRAINT     Q   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT pkey UNIQUE (id) INCLUDE (id);
 5   ALTER TABLE ONLY public.orders DROP CONSTRAINT pkey;
       public                 postgres    false    218            4           2606    16451    products products_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.products DROP CONSTRAINT products_pkey;
       public                 postgres    false    217            :           2606    16416    users user_pkey 
   CONSTRAINT     M   ALTER TABLE ONLY public.users
    ADD CONSTRAINT user_pkey PRIMARY KEY (id);
 9   ALTER TABLE ONLY public.users DROP CONSTRAINT user_pkey;
       public                 postgres    false    219            @           2606    16469 (   warehouse-product warehouse-product_pkey 
   CONSTRAINT     �   ALTER TABLE ONLY public."warehouse-product"
    ADD CONSTRAINT "warehouse-product_pkey" PRIMARY KEY (warehouse_id, product_id);
 V   ALTER TABLE ONLY public."warehouse-product" DROP CONSTRAINT "warehouse-product_pkey";
       public                 postgres    false    223    223            >           2606    16471    warehouses warehouses_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.warehouses
    ADD CONSTRAINT warehouses_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.warehouses DROP CONSTRAINT warehouses_pkey;
       public                 postgres    false    222            B           2606    16429    order-product order_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."order-product"
    ADD CONSTRAINT order_id_fkey FOREIGN KEY (order_id) REFERENCES public.orders(id) NOT VALID;
 G   ALTER TABLE ONLY public."order-product" DROP CONSTRAINT order_id_fkey;
       public               postgres    false    218    4662    220            C           2606    16453    order-product product_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."order-product"
    ADD CONSTRAINT product_id_fkey FOREIGN KEY (product_id) REFERENCES public.products(id) NOT VALID;
 I   ALTER TABLE ONLY public."order-product" DROP CONSTRAINT product_id_fkey;
       public               postgres    false    4660    217    220            D           2606    16477 !   warehouse-product product_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."warehouse-product"
    ADD CONSTRAINT product_id_fkey FOREIGN KEY (product_id) REFERENCES public.products(id) NOT VALID;
 M   ALTER TABLE ONLY public."warehouse-product" DROP CONSTRAINT product_id_fkey;
       public               postgres    false    4660    217    223            A           2606    16441    orders user_id_fkey    FK CONSTRAINT     |   ALTER TABLE ONLY public.orders
    ADD CONSTRAINT user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id) NOT VALID;
 =   ALTER TABLE ONLY public.orders DROP CONSTRAINT user_id_fkey;
       public               postgres    false    4666    219    218            E           2606    16472 #   warehouse-product warehouse_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public."warehouse-product"
    ADD CONSTRAINT warehouse_id_fkey FOREIGN KEY (warehouse_id) REFERENCES public.warehouses(id) NOT VALID;
 O   ALTER TABLE ONLY public."warehouse-product" DROP CONSTRAINT warehouse_id_fkey;
       public               postgres    false    4670    222    223            �   =   x�Uɱ�0��F )Fv��sA�"��7�GR�A�a�kõa����W�./D���V�      �   i   x�5̱C!�v1��Kd��|��?B�"��׋ae�HZ�����n�vS]^˂>h��|
X@?Q(ʶS�%�B��h�>2��[��6�N,�Q�Vk���      �   U   x��qW0475���44200�3���Ē+"3?7S��(�G�65����K�MU04126�g�4�I����M`rF\1z\\\ }��      �   �   x�mM;
�@����l�n��yO�&��`�V!����xA4gx{##�f��0#
���h���Pb
���9O�W6f��~�z��ݦɪ�p�>u��n�0�=u0��zxL|�3�a�#�l(�BΜ&Z���дf^������iڹ~��,�;�T9B�RO]+      �      x������ � �      �      x������ � �     