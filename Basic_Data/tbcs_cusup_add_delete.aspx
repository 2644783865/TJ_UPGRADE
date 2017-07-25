<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"
    CodeBehind="tbcs_cusup_add_delete.aspx.cs" Inherits="ZCZJ_DPF.Basic_Data.tbcs_cusup_add_delete" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="RightContentTitlePlace">
    <meta http-equiv="pragma" content="no-cache">
    <meta http-equiv="cache-control" content="no-cache">
    <meta http-equiv="expires" content="0">
    <table width="100%">
        <tr>
            <td>
                <p>
                    厂商信息添加/停用/启用</p>
            </td>
        </tr>
    </table>

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script src="../JS/StyleControl.js" type="text/javascript" charset="GB2312"></script>

    <script src="../JS/DatePicker.js" type="text/javascript" charset="GB2312"></script>

    <script type="text/javascript">
//    var input_qiyexinzhi_js;
//    var input_renzhentixi_js;
//    var input_caigoup_js;
//    window.onbeforeunload   =   function()   {
//        input_qiyexinzhi_js=document.getElementById('input_qiyexinzhi').value;
//        input_renzhentixi_js=document.getElementById('input_renzhentixi').value;
//        input_caigoup_js=document.getElementById('input_caigoup').value;
//    }
//    window.onload = function () {

//        var action_var= "<%=action%>";
//        if(action_var=="Delete"||action_var=="Review"||action_var=="View")
//        {
//            document.getElementById('<%= input_qiyexinzhi.ClientID %>').disabled=true
//            document.getElementById('<%= input_renzhentixi.ClientID %>').disabled=true
//            document.getElementById('<%= input_caigoup.ClientID %>').disabled=true


//        }
//        else
//        {
//           document.getElementById('<%= input_caigoup.ClientID %>').disabled=false
//   
//        }
//    
//    }
    
    //**********弹出技术部人员子窗口***********************
        var i;
        array = new Array();
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        function SelTechPersons2() {
            $("#hidPerson").val("first_add");
            SelPersons();
        }
        function SelTechPersons3() {
            $("#hidPerson").val("second");
            SelPersons();
        }
        function SelTechPersons4() {
            $("#hidPerson").val("second_add");
            SelPersons();
        }
        function SelTechPersons5() {
            $("#hidPerson").val("third");
            SelPersons();
        }


        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=ddl_onePer.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            if (id == "first_add") {
                $("#<%=ddl_oneper_add.ClientID %>").val(r.st_name);
                $("#<%=first_addid.ClientID %>").val(r.st_id);
            }
            if (id == "second") {
                $("#<%=ddl_two.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
           if (id == "second_add") {
                $("#<%=ddl_two_add.ClientID %>").val(r.st_name);
                $("#<%=second_addid.ClientID %>").val(r.st_id);
            }
            if (id == "third") {
                $("#<%=ddl_three.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            $('#win').dialog('close');
        }
        
        
   function qiyexinzhi() {
            var cbl = document.getElementById('<%= CheckBoxList_qiyexinzhi.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");

            for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
 
            for(var k = 0; k < inputs.length; k++)
            {
                if (inputs[k].type == "checkbox" && inputs[k].checked) {
                    if(k==inputs.length-1){
                       document.getElementById('<%= input_qiyexinzhi.ClientID %>').value ="";   
                       document.getElementById('<%= input_qiyexinzhi.ClientID %>').disabled=false
                    }
                    else{
                       document.getElementById('<%= input_qiyexinzhi.ClientID %>').value ="";   
                       document.getElementById('<%= input_qiyexinzhi.ClientID %>').disabled=true
                                     
                    }
                }
               else{
                       document.getElementById('<%= input_qiyexinzhi.ClientID %>').value ="";                                       
                       document.getElementById('<%= input_qiyexinzhi.ClientID %>').disabled=true

                }
            }
           }
  function renzhentixi() {
            var cbl1 = document.getElementById('<%= CheckBoxList_renzhentixi.ClientID %>')
            var inputs1 = cbl1.getElementsByTagName("input");

            for(var i=0;i<inputs1.length;i++)
            {
                var chk  = inputs1[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
            for(var k = 0; k < inputs1.length; k++)
            {
                if (inputs1[k].type == "checkbox" && inputs1[k].checked) {
                    if(k==inputs1.length-1){
                       document.getElementById('<%= input_renzhentixi.ClientID %>').disabled=false
                    }
                    else{
                       document.getElementById('<%= input_renzhentixi.ClientID %>').value="";  
                       document.getElementById('<%= input_renzhentixi.ClientID %>').disabled=true
                                      
                    }
                }
               else{
                       document.getElementById('<%= input_renzhentixi.ClientID %>').value="";  
                       document.getElementById('<%= input_renzhentixi.ClientID %>').disabled=true
             
                }
                
            }
            }
            
    var change1 = function () {
            var cbl = document.getElementById('<%= CheckBoxList1.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");

            for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
            }
   var change2 = function () {
            var cbl = document.getElementById('<%= CheckBoxList2.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");

            for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
            }
 var change3 = function () {
            var cbl = document.getElementById('<%= CheckBoxList3.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");

             for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
            }
 var change4 = function () {
            var cbl = document.getElementById('<%= CheckBoxList4.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");

            for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
            }
 var change5 = function () {
            var cbl = document.getElementById('<%= CheckBoxList5.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");

            for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
            }
 var change6  = function () {
            var cbl = document.getElementById('<%= CheckBoxList6.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");

            for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
            }
 var change7 = function () {
            var cbl = document.getElementById('<%= CheckBoxList7.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");

            for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
            }
 var change8 = function () {
            var cbl = document.getElementById('<%= CheckBoxList8.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");

            for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }
            }
 var change9 = function () {
            var cbl = document.getElementById('<%= CheckBoxList9.ClientID %>')
            var inputs = cbl.getElementsByTagName("input");
            for(var i=0;i<inputs.length;i++)
            {
                var chk  = inputs[i];
                if(chk != event.srcElement)
                {
                    chk.checked = false;
                }
            }

            
            
            
        }
    
    </script>

</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PrimaryContent">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <%--<asp:UpdatePanel ID="update_body" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div class="RightContent">
        <div class="box-inner">
            <div class="box_right">
                <div class="box-title">
                    <table width="100%">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnLoad" runat="server" Text="提 交" OnClick="btnLoad_Click" CssClass="button-outer" />
                                &nbsp;&nbsp;
                                <asp:Button ID="btnReturn" runat="server" Text="返 回" CausesValidation="False" CssClass="button-outer"
                                    OnClick="btnReturn_Click" />
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
            ActiveTabIndex="1">
            <cc1:TabPanel ID="Tab_INFO" runat="server" Width="100%" HeaderText="厂商信息" TabIndex="0">
                <ContentTemplate>
                    <asp:Panel ID="Pal_info" runat="server">
                        <asp:Label ID="Label1" runat="server" Text="厂商信息"></asp:Label>(带<span class="Error">*</span>号的为必填项)
                        <div class="box-wrapper">
                            <div class="box-outer">
                                <%--<asp:Panel ID="Pal_CODE" runat="server">
                                            <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                                <tr>
                                                    <td width="135px">
                                                        公司编号
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCS_CODE" runat="server" Enabled="False" Width="246px"></asp:TextBox>
                                                        <span class="Error">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                            runat="server" ErrorMessage="公司编号不能为空！" ControlToValidate="txtCS_CODE" Display="Dynamic"></asp:RequiredFieldValidator><asp:Button
                                                                ID="btnCreatID" runat="server" Text="生成公司编号" CausesValidation="False" OnClick="btnCreatID_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>--%>
                                <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                                    <tr>
                                        <td width="135px">
                                            编号
                                        </td>
                                        <td>
                                            <asp:TextBox ID="purchase_identifier" runat="server" Width="246px"></asp:TextBox>
                                        </td>
                                        </tr>
                                        <tr>
                                        <td width="135px">
                                            公司名称
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_NAME" runat="server" Width="246px"></asp:TextBox>
                                            <asp:TextBox ID="txtCS_CODE" runat="server" Visible="false" Width="246px"></asp:TextBox>
                                            <span class="Error">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                runat="server" ErrorMessage="请输入公司名称！" ControlToValidate="txtCS_NAME"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            所在地
                                        </td>
                                        <td>
                                            <p>
                                                <asp:TextBox ID="txtCS_LOCATION" runat="server" ReadOnly="True" Enabled="False" Width="246px"></asp:TextBox><span
                                                    class="Error">*</span>
                                                <%--                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                ErrorMessage="请选择所在地！" ControlToValidate="txtCS_LOCATION"></asp:RequiredFieldValidator>--%>
                                                <asp:Panel ID="palLocation" runat="server">
                                                    <asp:DropDownList ID="dopCL_LOCATION" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dopCL_LOCATION_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    &nbsp;省
                                                    <asp:DropDownList ID="dopCL_LOCATION_NEXT" runat="server" AutoPostBack="True" OnTextChanged="dopCL_LOCATION_NEXT_TextChanged">
                                                    </asp:DropDownList>
                                                    &nbsp;（市/区）</asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            助记码
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_HRCODE" runat="server" Width="246px"></asp:TextBox><span class="Error">
                                                *</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="请填写助记码！"
                                                ControlToValidate="txtCS_HRCODE"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            所属类型
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_TYPE" runat="server" ReadOnly="True" Enabled="False" Width="246px"></asp:TextBox><span
                                                class="Error">*[必选项]</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                            <asp:DropDownList ID="dopCS_TYPE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dopCS_TYPE_SelectedIndexChanged">
                                                <asp:ListItem Text="-未选择-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="客户" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="采购供应商" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="运输公司" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="技术外协分包商" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="生产外协分包商" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="原材料销售供应商" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="其它" Value="7"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            通讯地址
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_ADDRESS" runat="server" Width="246px"></asp:TextBox><span
                                                class="Error"> *</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCS_ADDRESS"
                                                ErrorMessage="请输入通讯地址！" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            公司联系电话
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_PHONO" runat="server" Width="246px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            公司联系人姓名
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_CONNAME" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            公司邮箱
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_MAIL" runat="server" Width="246px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            邮编
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_ZIP" runat="server" Width="246px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            传真
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_FAX" runat="server" Width="246px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            产品所属类型
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_COREBS" runat="server" Height="49px" TextMode="MultiLine"
                                                Width="246px"></asp:TextBox><span class="Error"> *</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填项！"
                                                ControlToValidate="txtCS_COREBS"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            所供产品
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TB_Scope" runat="server" Height="49px" TextMode="MultiLine" Width="246px"></asp:TextBox>
                                            <span class="Error">所供产品之间请用中文逗号“，”隔开</span>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                                <td>
                                                    重要等级
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCS_RANK" runat="server" ReadOnly="True" Enabled="False" Width="246px"></asp:TextBox><span
                                                        class="Error">*[必选项]</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:DropDownList ID="dopCS_RANK" runat="server" AutoPostBack="True" OnSelectedIndexChanged="dopCS_RANK_SelectedIndexChanged">
                                                        <asp:ListItem Text="-未选择-" Value="-未选择-"></asp:ListItem>
                                                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%>
                                    <tr>
                                        <td>
                                            开户行：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_BANK" runat="server" Width="246px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            开户行帐号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_ACCOUNT" runat="server" Width="246px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            税号：
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_TAX" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            申请人
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_MANCLERK" runat="server" Enabled="False" Width="246px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            申请日期
                                        </td>
                                        <td>
                                            <input id="txtCS_FILLDATE" type="text" runat="server" disabled="disabled" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            备注
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCS_NOTE" runat="server" TextMode="MultiLine" Height="49px" Width="246px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </asp:Panel>
                    <table align="center" cellpadding="4" cellspacing="1" class="toptable grid" border="1">
                        <tr>
                            <td style="width: 150px">
                                附件：
                            </td>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:Label ID="lblpathName" runat="server" Visible="false"></asp:Label>
                                <asp:Button ID="bntupload" runat="server" Text="上 传" OnClick="bntupload_Click" CausesValidation="False" />
                                <asp:Label ID="filesError" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lbreport" runat="server" Visible="False"></asp:Label>
                                <asp:GridView ID="gvfileslist" runat="server" AutoGenerateColumns="False" PageSize="5"
                                    CssClass="toptable grid" CellPadding="4" DataKeyNames="ID" ForeColor="#333333"
                                    Width="100%">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# Eval("ID") %>' ID="lbid"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FILENAME" HeaderText="文件名称" ItemStyle-Width="60%">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UPLOADDATE" HeaderText="上传时间">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="删除">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtndelete" runat="server" ImageUrl="~/Assets/images/erase.gif"
                                                    OnClick="imgbtndelete_Click" CausesValidation="False" ToolTip="删除" Width="15px"
                                                    Height="15px" />
                                            </ItemTemplate>
                                            <ControlStyle Font-Size="Small" />
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="下载">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtndownload" runat="server" ImageUrl="~/Assets/images/pdf.jpg"
                                                    OnClick="imgbtndownload_Click" CausesValidation="False" ToolTip="下载" Width="15px"
                                                    Height="15px" />
                                            </ItemTemplate>
                                            <ControlStyle Font-Size="Small" />
                                            <ItemStyle HorizontalAlign="Center" Width="120px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" Font-Size="Small"
                                        Height="10px" />
                                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table id="shanchu_beizhu_table" runat="server" align="center" cellpadding="4" cellspacing="1"
                        class="toptable grid" border="1" visible="false">
                        <tr>
                            <td style="width: 150px">
                                停用原因
                            </td>
                            <td style="width: 150px">
                                <asp:TextBox ID="shanchu_beizhu" runat="server" TextMode="MultiLine" Width="500px"
                                    Height="70px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="Tab_Review" runat="server" HeaderText="评审信息" TabIndex="1">
                <ContentTemplate>
                    <asp:Panel ID="Pal_Review" runat="server">
                        <table width="100%">
                            <tr>
                                <td>
                                    指定评审人员
                                </td>
                            </tr>
                        </table>
                        <div class="box-wrapper">
                            <div class="box-outer">
                                <asp:Panel ID="Pal_person" runat="server">
                                    <table style="width: 100%" cellpadding="4" class="toptable grid" cellspacing="1"
                                        border="1">
                                        <tr>
                                            <td style="width: 35%" align="center">
                                                生产部审批：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ddl_onePer" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                                    <asp:Image ID="AddImage12" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 35%" align="center">
                                                设备部审批：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ddl_oneper_add" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="first_addid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hand" onClick="SelTechPersons2()">
                                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 35%" align="center">
                                                技术部审批：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ddl_two" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="hand" onClick="SelTechPersons3()">
                                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 35%" align="center">
                                                采购部审批：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ddl_two_add" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="second_addid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="HyperLink3" runat="server" CssClass="hand" onClick="SelTechPersons4()">
                                                    <asp:Image ID="Image3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 35%" align="center">
                                                主管经理审批：
                                            </td>
                                            <td>
                                                <asp:TextBox ID="ddl_three" runat="server" Width="80px" Enabled="false"></asp:TextBox>
                                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="HyperLink4" runat="server" CssClass="hand" onClick="SelTechPersons5()">
                                                    <asp:Image ID="Image4" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择
                                                </asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Pal_Result" runat="server">
                                    <table id="tab" cellpadding="2" cellspacing="1" class="toptable grid" border="1"
                                        width="100%">
                                        <asp:Panel ID="YIJI" runat="server">
                                            <tr align="left">
                                                <td style="width: 135px">
                                                    提出部门：
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lbl_tcbm" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left">
                                                <td>
                                                    生产部评审意见
                                                </td>
                                                <td colspan="2">
                                                    <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                                                        <tr>
                                                            <td>
                                                                <span>意见：</span>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="RadioTY1" runat="server" Text="同意" GroupName="select1" AutoPostBack="true"
                                                                    OnCheckedChanged="BMYJ_CheckedChanged" />
                                                                <asp:RadioButton ID="RadioJJ1" runat="server" Text="拒绝" GroupName="select1" AutoPostBack="true"
                                                                    OnCheckedChanged="BMYJ_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>备注：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBZ1" runat="server" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>审核人：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHR1" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <span>审核日期：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHRQ1" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="ERJI" runat="server">
                                            <tr align="left">
                                                <td>
                                                    设备部评审意见
                                                </td>
                                                <td colspan="2">
                                                    <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                                                        <tr>
                                                            <td>
                                                                <span>意见：</span>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="RadioTY2" runat="server" Text="同意" GroupName="select2" AutoPostBack="true"
                                                                    OnCheckedChanged="EJYJ_CheckedChanged" />
                                                                <asp:RadioButton ID="RadioJJ2" runat="server" Text="拒绝" GroupName="select2" AutoPostBack="true"
                                                                    OnCheckedChanged="EJYJ_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>备注：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBZ2" runat="server" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>审核人：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHR2" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <span>审核日期：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHRQ2" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="SANJI" runat="server">
                                            <tr>
                                                <td>
                                                    技术部评审意见
                                                </td>
                                                <td colspan="2">
                                                    <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                                                        <tr>
                                                            <td>
                                                                <span>意见：</span>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="RadioTY3" runat="server" Text="同意" GroupName="select3" AutoPostBack="true"
                                                                    OnCheckedChanged="SJYJ_CheckedChanged" />
                                                                <asp:RadioButton ID="RadioJJ3" runat="server" Text="拒绝" GroupName="select3" AutoPostBack="true"
                                                                    OnCheckedChanged="SJYJ_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>备注：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBZ3" runat="server" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>审核人：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHR3" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <span>审核日期：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHRQ3" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="SIJI" runat="server">
                                            <tr>
                                                <td>
                                                    采购部评审意见
                                                </td>
                                                <td colspan="2">
                                                    <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                                                        <tr>
                                                            <td>
                                                                <span>意见：</span>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="RadioTY4" runat="server" Text="同意" GroupName="select4" AutoPostBack="true"
                                                                    OnCheckedChanged="SIJ_YJ_CheckedChanged" />
                                                                <asp:RadioButton ID="RadioJJ4" runat="server" Text="拒绝" GroupName="select4" AutoPostBack="true"
                                                                    OnCheckedChanged="SIJ_YJ_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>备注：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBZ4" runat="server" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>审核人：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHR4" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <span>审核日期：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHRQ4" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="WUJI" runat="server">
                                            <tr>
                                                <td>
                                                    领导评审意见
                                                </td>
                                                <td colspan="2">
                                                    <table cellpadding="2" cellspacing="1" class="toptable grid" border="1" width="100%">
                                                        <tr>
                                                            <td>
                                                                <span>意见：</span>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="RadioTY5" runat="server" Text="同意" GroupName="select5" AutoPostBack="true"
                                                                    OnCheckedChanged="WUJ_YJ_CheckedChanged" />
                                                                <asp:RadioButton ID="RadioJJ5" runat="server" Text="拒绝" GroupName="select5" AutoPostBack="true"
                                                                    OnCheckedChanged="WUJ_YJ_CheckedChanged" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>备注：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextBZ5" runat="server" TextMode="MultiLine" Width="500px" Height="70px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span>审核人：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHR5" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <span>审核日期：</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TextSHRQ5" runat="server" Enabled="false"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="供方评价记录表" TabIndex="1">
                <ContentTemplate>
                    <asp:Panel ID="charge_note" runat="server">
                        <table width="100%" border="1" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="padding-left: 40px;">
                                    企业性质：
                                </td>
                                <td>
                                    <ul style="list-style-type: none; height: 0px; margin: 0px">
                                        <li style="float: left">
                                            <asp:CheckBoxList ID="CheckBoxList_qiyexinzhi" runat="server" RepeatColumns="6" RepeatDirection="Horizontal"
                                                onClick="javascript:qiyexinzhi()">
                                                <asp:ListItem Text="外商独资" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="合资" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="国有" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="集体" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="民营" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="其他" Value="5"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </li>
                                        <li style="float: left; margin-top: 7px;">
                                            <asp:TextBox ID="input_qiyexinzhi" runat="server" Enabled="false"></asp:TextBox></li>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td style="line-height: 30px; padding-left: 20px;">
                                    本公司拟采购的产品：
                                </td>
                                <td style="padding-left: 20px;">
                                    <asp:TextBox ID="input_caigoup" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="line-height: 30px; padding-left: 20px;">
                                    供方简介及质量能力评价：
                                </td>
                            </tr>
                            <tr style="height: 36px">
                                <td style="padding-left: 40px;">
                                    通过认证体系：
                                </td>
                                <td>
                                    <ul style="list-style-type: none; height: 0px; margin: 0px">
                                        <li style="float: left">
                                            <asp:CheckBoxList ID="CheckBoxList_renzhentixi" runat="server" RepeatColumns="4"
                                                RepeatDirection="Horizontal" onClick="javascript:renzhentixi()">
                                                <asp:ListItem Text="IS09001" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="IS014001" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="OHSAS18001" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="其他" Value="3"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </li>
                                        <li style="float: left; margin-top: 7px;">
                                            <asp:TextBox ID="input_renzhentixi" runat="server" Enabled="false"></asp:TextBox></li>
                                    </ul>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    产品质量状况：
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                        onClick="javascript:change1()">
                                        <asp:ListItem Text="优秀" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="良好" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="一般" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="较差" Value="3"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    行业内知名度：
                                    <%--  //18526781692--%>
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                        onClick="javascript:change2()">
                                        <asp:ListItem Text="很高" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="较高" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="一般" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="较差" Value="3"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    其他顾客满意度：
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:CheckBoxList ID="CheckBoxList3" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                        onClick="javascript:change3()">
                                        <asp:ListItem Text="优秀" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="良好" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="一般" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="较差" Value="3"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    技术能力：
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:CheckBoxList ID="CheckBoxList4" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                        onClick="javascript:change4()">
                                        <asp:ListItem Text="强" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="一般" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="较差" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="不涉及" Value="3"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    装备能力：
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:CheckBoxList ID="CheckBoxList5" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                        onClick="javascript:change5()">
                                        <asp:ListItem Text="满足要求" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="基本满足" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不满足" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="不涉及" Value="3"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    生产能力：
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:CheckBoxList ID="CheckBoxList6" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                        onClick="javascript:change6()">
                                        <asp:ListItem Text="满足要求" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="基本要求" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="不满足" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="不涉及" Value="3"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    服务承诺：
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:CheckBoxList ID="CheckBoxList7" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                        onClick="javascript:change7()">
                                        <asp:ListItem Text="优秀" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="良好" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="一般" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="较差" Value="3"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    技术支持能力：
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:CheckBoxList ID="CheckBoxList8" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                        onClick="javascript:change8()">
                                        <asp:ListItem Text="较好" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="一般" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="较差" Value="2"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 40px;">
                                    价格：
                                </td>
                                <td style="padding-left: 10px;">
                                    <asp:CheckBoxList ID="CheckBoxList9" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                                        onClick="javascript:change9()">
                                        <asp:ListItem Text="较高" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="合理" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="较低" Value="2"></asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
    <div>
        <div>
            <div id="win" visible="false">
                <div>
                    <table>
                        <tr>
                            <td>
                                <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                按部门查询：
                            </td>
                            <td>
                                <input id="dep" name="dept" value="03">
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="width: 430px; height: 230px">
                    <table id="dg">
                    </table>
                </div>
            </div>
            <div id="buttons" style="text-align: right" visible="false">
                <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="return savePick();">
                    保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                        onclick="javascript:$('#win').dialog('close')">取消</a>
            </div>
        </div>
        <input type="hidden" id="hidPerson" value="" />
    </div>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
