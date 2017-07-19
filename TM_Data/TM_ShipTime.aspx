<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master" CodeBehind="TM_ShipTime.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_ShipTime" %>

<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
<script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script> 
    </asp:Content>       
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
<script type="text/javascript" >
    function CheckTheNum(obj)
    {
        var text=obj.value;
        var pattem=/^-?[1-9]+(\.\d+)?$|^-?0(\.\d+)?$|^-?[1-9]+[0-9]*(\.\d+)?$/; 
        if(text!="")
        {
            if(!pattem.test(text))
            {
                alert('输入格式有误!');
                obj.value="0";
                return false;
            }
        }
    }

    function show(input)
    {
      var aa=window.showModalDialog("TM_ShipTimeDetail.aspx?id=<%=tsaid.Text %>","","DialogWidth=1000px;DialogHeight=550px;status:no;"); 
           array=new Array();
      if(aa=="Refesh")
       {
          window.location.reload();
       }
    var table=document.getElementById ("<%= GridView1.ClientID %>");
    var tr=table.getElementsByTagName("tr");
    var index=input.parentNode.parentNode.getElementsByTagName("td")[1].getElementsByTagName("span")[0].innerText;
    if(aa==undefined)
    {
        if(tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value!=""||tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value!="")
        {
        aa=(tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value)+'+'+(tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value);
        }
        else
        {
        aa="+0";
        }
    }
    array=aa.split('+');
    tr[index].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=array[0];
    tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value=array[1]; 
    __doPostBack(tr[index].getElementsByTagName("td")[8].getElementsByTagName("input")[0].id,'');
    }
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
     <div class="box-wrapper">
     <div class="box-inner">
         <div class="box_right">
             <div class="box-title">
                 <table width="100%" >
                    <tr><td>发运信息表</td></tr>
                 </table>
             </div>
         </div>
     </div>
        <div class="box-outer">
        <div>
    <table width="100%" align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
        <tr>
        <td class="tdleft1">生产制号:</td>     
        <td class="tdright1"><asp:Label ID="tsaid" runat="server"></asp:Label>
        <td class="tdleft1">项目名称:</td>          
        <td class="tdright1"><asp:Label ID="proname" runat="server"></asp:Label>
          <asp:Label ID="lblpjid" runat="server" Visible="false" ></asp:Label>
        </td>
       </tr>
        <tr>
        <td class="tdleft1">工程名称:</td>     
        <td class="tdright1"><asp:Label ID="engname" runat="server"></asp:Label></td>
        <td class="tdleft1">工程类型:</td>     
        <td class="tdright1"><asp:Label ID="ddlengtype" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="tdleft1">数    量:</td>     
            <td class="tdright1"><asp:Label ID="num" runat="server" ></asp:Label></td>
            <td class="tdleft1">总  重(kg):</td>          
            <td class="tdright1"><asp:Label ID="totweight" runat="server" ></asp:Label><span style="color:Red;">(各船次重量之和必须等于总重)</span></td>
            <asp:Label ID="lblkey" runat="server" Visible="false" ></asp:Label>
           </tr>
 <tr>
  <td colspan="2">
               <asp:Button ID="btnAdd" runat="server" Text="添 加" onclick="btnAdd_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnDelete" runat="server" Text="删 除" OnClientClick="return confirm('确认删除吗？');" onclick="btnDelete_Click"/>                       
  </td>
  <td colspan="2" align="right">
               <asp:Button ID="btnConfirm" runat="server" Text="保 存" OnClientClick="return confirm('确认保存吗？\r\r提示:【集港时间】在今天之前的记录将无法修改,请核实！！！')" onclick="btnConfirm_Click"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button ID="btnCancel" runat="server" Text="返 回" onclick="btnCancel_Click"/>   
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
               <asp:Button ID="btnFinsh" runat="server" Text="发运完成" onclick="btnFinsh_Click" />     
              <input id="txtsave" runat="server" style="display:none;" value="0" />           
  </td>
 </tr>
 <tr>
 <td colspan="4">
 <asp:GridView ID="GridView1" width="100%" CssClass="toptable grid" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                    ShowFooter="True" OnRowDataBound="GridView1_RowDataBound">
            <AlternatingRowStyle BackColor="White" />                    
            <Columns>
                 <asp:TemplateField  ItemStyle-Width="12px">
                    <ItemTemplate>
                        <asp:CheckBox ID="cbcheck" runat="server" Width="10px"  />
                    </ItemTemplate> 
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="序号">
                    <ItemTemplate>
                        <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField DataField="TSA_FY" ItemStyle-HorizontalAlign="Center" HeaderText="发运" ItemStyle-Wrap="false" />
                <asp:TemplateField HeaderText="船次编号" ItemStyle-Wrap="false">
                    <ItemTemplate>
                       <asp:TextBox ID="txtship" ToolTip="生产制号-船次号,如:CO/CX/1-1" runat="server" onfocus="this.select();" Text='<%#Eval("TSA_SHIP") %>'></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="设备名称" ItemStyle-Wrap="false">
                    <ItemTemplate>
                       <asp:TextBox ID="txtsbno"  runat="server"  onfocus="this.select();" Text='<%#Eval("TSA_DEVICENO") %>'></asp:TextBox>
                       <asp:HyperLink ID="hlSelect" runat="server" CssClass="hand" onClick="show(this);">
                           <asp:Image ID="Image1" ImageUrl="~/Assets/images/res.gif"  runat="server" />
                        </asp:HyperLink></td>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="体积(m³)" ItemStyle-Wrap="false" >
                    <ItemTemplate>
                       <asp:TextBox ID="txtsbtj" runat="server" Text='<%#Eval("TSA_VOLUME") %>'  onfocus="this.select();"  Width="70px" OnTextChanged="txt_textchanged" AutoPostBack="true"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="毛重(kg)" ItemStyle-Wrap="false">
                    <ItemTemplate>
                       <asp:TextBox ID="txtsbmz" runat="server" Text='<%#Eval("TSA_GROSSWGHT") %>'  onfocus="this.select();"  Width="70px" OnTextChanged="txt_textchanged" AutoPostBack="true"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="净重量(kg)" ItemStyle-Wrap="false">
                    <ItemTemplate>
                       <asp:TextBox ID="txtsbjz" runat="server" Text='<%#Eval("TSA_NETWGHT") %>'  onfocus="this.select();"  Width="70px" OnTextChanged="txt_textchanged" AutoPostBack="true"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="船次重量(kg)" ItemStyle-Wrap="false">
                    <ItemTemplate>
                       <asp:TextBox ID="txtweight" runat="server" Text='<%#Eval("TSA_SHIPWGHT") %>'  onfocus="this.select();"  onchange="CheckTheNum(this)" Width="70px" OnTextChanged="txt_textchanged" AutoPostBack="true"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="计划时间" ItemStyle-Wrap="false">
                    <ItemTemplate>
                       <asp:TextBox ID="txtshipplantime" runat="server" Text='<%#Eval("TSA_SHIPPLANTIME") %>'  onfocus="this.select();"  onclick="setday(this);"></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="集港时间" ItemStyle-Wrap="false">
                    <ItemTemplate>
                       <asp:TextBox ID="txtshiptime" runat="server" Text='<%#Eval("TSA_SHIPTIME") %>'  onfocus="this.select();"  onclick="setday(this);" ></asp:TextBox>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="隐藏列" ItemStyle-Wrap="false" Visible="false" >
                    <ItemTemplate>
                       <asp:Label ID="lblid"  runat="server" Text='<%#Eval("TSA_NO") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <HeaderStyle BackColor="#B9D3EE" Font-Bold="True"  ForeColor="#1E5C95" />
            <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
            <PagerStyle  CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
 </td>
 </tr>
  
  </table>
</div>
    </div>
   </div> 
   </ContentTemplate>
   </asp:UpdatePanel>
   </asp:Content> 
