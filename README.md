# Layui-WPF
这是一个WPF版的Layui前端UI样式库
![image](https://user-images.githubusercontent.com/37786276/174557918-fffdf048-0a37-4a06-8806-a41a84922ef6.png)
![image](https://user-images.githubusercontent.com/37786276/174557939-13bb66f2-7053-4a99-88da-5e787c4ef959.png)
![image](https://user-images.githubusercontent.com/37786276/174557954-0876d288-df21-4daf-9f0e-b30a2e069a5e.png)
![image](https://user-images.githubusercontent.com/37786276/174557974-292577b9-c0ed-42fa-9774-92418f3a1072.png)
![image](https://user-images.githubusercontent.com/37786276/174557992-db9a214a-2f19-4b63-abb4-2bde4cec15b4.png)
![image](https://user-images.githubusercontent.com/37786276/174558010-7b9db666-93c6-4f39-8707-4f504b094cef.png)
![image](https://user-images.githubusercontent.com/37786276/174558034-1f86f4b9-a8b9-401e-a75c-e2f832b17439.png)
![image](https://user-images.githubusercontent.com/37786276/174558072-53fac753-7a98-4de2-af56-bcb6199214c8.png)
![image](https://user-images.githubusercontent.com/37786276/174558089-f723827e-0dbd-4f81-b476-c664e79e4731.png)
![image](https://user-images.githubusercontent.com/37786276/174558126-f1970cbf-ad43-4952-97e1-491af81ba0cc.png)
![image](https://user-images.githubusercontent.com/37786276/174558161-95c41ee3-62d5-41f4-a656-da7ef59d4d53.png)
![image](https://user-images.githubusercontent.com/37786276/174558214-b4b5b159-293c-4bff-86b7-da6a6a46edfb.png)
![image](https://user-images.githubusercontent.com/37786276/174558242-51212ab7-ae81-423d-8760-4b001ca8c4d8.png)
![image](https://user-images.githubusercontent.com/37786276/174558274-fe8b4667-f39e-4d12-9fca-6d28b25f7444.png)
![image](https://user-images.githubusercontent.com/37786276/174558295-d2a1d372-2b14-4a64-ac44-169cde7344bc.png)
![image](https://user-images.githubusercontent.com/37786276/175768134-5d8a8af4-9d75-4316-a14c-95c1cad5232c.png)
![image](https://user-images.githubusercontent.com/37786276/183010871-5c359d7b-1790-4647-87e1-21ee51e3b423.png)
![录制_2022_09_02_12_14_16_634](https://user-images.githubusercontent.com/37786276/188057815-b26e85f5-4771-4ad5-9b01-d08425cea675.gif)
![image](https://user-images.githubusercontent.com/37786276/188057860-ff5397ca-9400-4550-8f6d-91759f26a0bd.png)
![image](https://user-images.githubusercontent.com/37786276/188057885-58248e5d-597a-45fc-875c-c8cc9ecee905.png)
![image](https://user-images.githubusercontent.com/37786276/188057922-b66e7cec-c814-4759-83af-bcfc579287d2.png)
![image](https://user-images.githubusercontent.com/37786276/188057976-0640e149-dd7a-4034-9284-436ed8ee336a.png)
![image](https://user-images.githubusercontent.com/37786276/188057986-e5d1ede3-1c6e-45c5-85b6-c5817b442d3c.png)
![image](https://user-images.githubusercontent.com/37786276/188058016-5fecef93-686e-4725-9e44-1fcb3d3e53ee.png)
![image](https://user-images.githubusercontent.com/37786276/195978771-3fc40dc3-a7af-48cf-aac0-3a92a67909d6.png)
![image](https://user-images.githubusercontent.com/37786276/209177671-9bfa0971-adf0-41c8-8fe4-0814fb53d2bc.png)
![image](https://user-images.githubusercontent.com/37786276/209177898-a28f4524-17f2-4d94-9b8b-cf837de5fa94.png)
![image](https://user-images.githubusercontent.com/37786276/210231504-d0f2acf5-60fc-450a-b913-1476818c6a1a.png)


## Usage

Step 1: 添加LayUI.Wpf Nuget包;

```Install-Package LayUI.Wpf```

Step 2: 添加代码在 App.xaml 下面:
```XML
<Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ResourceDictionary Source="pack://application:,,,/LayUI.Wpf;component/Themes/Default.xaml" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
</Application.Resources>
```

Step 3: 在目标页面添加需要的控件引用:
`xmlns:Lay="clr-namespace:LayUI.Wpf.Controls;assembly=LayUI.Wpf"`

Step 4: 完成
