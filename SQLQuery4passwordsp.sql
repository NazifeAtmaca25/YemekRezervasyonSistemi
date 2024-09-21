create proc password_sp
(
@tc varchar(50),
@email varchar(50)
)
as 
begin
 select count(*) from users where @tc=tc and @email=email
 end