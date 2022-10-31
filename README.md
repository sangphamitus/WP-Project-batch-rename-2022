# WP-Project-batch-rename-2022

**Mục tiêu đồ án:**
>	- Rename file hàng loạt theo một trật tự người dùng quyết định 
>	- Có thể chọn file, folder tùy ý
>	- Có thể tự thiết kế giao diện, miễn đúng chức năng
---

**Quy tắc đổi tên:**
>>	1- Đổi đuôi đã quy định trước 
>>>		ví dụ: .pdf -> .txt ( toàn bộ file .pdf sẽ đổi qua .txt)
>>	2- Thêm biến counter vào cuối file 01,02,03 => 001, 00001
>
>>	3- Xóa hết khoảng trắng trước và sau file name
>>>	 vd: _____ HELO__W___.txt => HELO__W.txt
>
>>	4- Đổi ký tự thành 1 ký tự khác
>
>>	5- Thêm tiền tố
>>>	vd: test.pdf -> thêm tiền tố N_ => N_test.pdf
>
>>	6- Thêm hậu tố
>>>	vd; test.pdf -> Thêm hậu tố _s => test_s.pdf
>
>>	7- Thành lowercase hết
>>>	vd: AsTA.txt => asta.txt
>
>>	8- Xóa hết khoảng trắng
>>>	 vd: _____ HELO__W___.txt => HELOW.txt
>
>>	9- Pascal case file name // K biet lam
	
**Yêu cầu chính : (7 điểm)**
>	- Save và load config trong file DLL
>	- Chọn files, folder muốn rename
>	- Làm chức năng trong quy tắc đổi tên
>>		+ Luật có thể add từ trong menu
>>		+ Mỗi luật, có tham số để thay đổi
>	- Đổi tên file theo cái rule đã chọn (từng file)

**Yêu cầu phụ ( mỗi yêu cầu +1 điểm)**
>>	1- Drag + Drop file trực tiếp từ giao diện
>
>>	2- Lưu file paramenters cho đổi tên dùng XML file / JSON / excel / database
>
>>	3- Đệ quy: đọc folder con
>
>>	4- Xử lý, duplication : không cho đổi tên file trùng -> phải có (1) (2)..
>
>>	5- Lưu lại vị trí cuối cùng khi tắt 
>>>		+Size
>>>		+ Vị trí trên màn hình 
>>>		+ Cái file config cuối cùng load
>
>>	6- Auto lưu và load cái trạng thái làm việc cuối cùng, khi nguồn điện ngắt đột ngột
>
>>	7- Dùng regex
>
>>	8- Có exceptions khi edit: 
>>>		+ Số lượng char <=255 char
>>>		+ ;/?{}|\
>
>>	9- Trùng Ý 6
>
>>	10- Xem preview trước khi rename
>
>>	11- Có thể copy file đã rename rồi vào folder được chọn ( k đụng tới file gốc)

---
	
**Thứ tự thực hiện:**
> 1- Load file + folder 
> 
> 2- Code rule + đổi tên + preview
> 
> 3- Code Config
> 
> 4- Giao diện

---
## Quy định :
> **Chưa hoàn thành:**
>> - [ ]  <Tên công việc> - <Tên người phân công> 
>>
>> **vd**:
>>  - [ ]  Làm giao diện - Lê Thị A
>> ---
> **Đã hoàn thành:**
>> - [X]  <Tên công việc> - <Tên người phân công> - <Ngày hoàn thành>
>>
>> **vd**:
>>  - [X]  Làm giao diện - Lê Thị A - 01/10/2022

----
# Lưu ý:
> - Sử dụng Design patterns: Singleton, Factory, Abstract factory, prototype
> - Plugin architecture (Nuget, Fody, Ribbon,...)
> - Delegate & event
---
## Phân việc:
>> - [X]  3 Rules (1,3,4) - Sương - 31/10/2022
>> - [ ]  3 Rules (5,6,7) - Yến
>> - [ ]  2 Rules (2,8) - Trí
>> - [X]  Load file + folder + giao diện tương ứng - Sang - 29/10/2022






