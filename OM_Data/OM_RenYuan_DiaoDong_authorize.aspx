<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_RenYuan_DiaoDong_authorize.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_RenYuan_DiaoDong_authorize" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    人员调动情况
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var i;
        array = new Array();
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }
        function SelTechPersons2() {
            $("#hidPerson").val("second");
            SelPersons();
        }
        function SelTechPersons3() {
            $("#hidPerson").val("third");
            SelPersons();
        }
        function SelTechPersons4() {
            $("#hidPerson").val("fourth");
            SelPersons();
        }
        function SelTechPersons5() {
            $("#hidPerson").val("fifth");
            SelPersons();
        }
        function SelTechPersons6() {
            $("#hidPerson").val("sixth");
            SelPersons();
        }

        function PerChange(input) {
            var arr = $(input).val().split('|');
            $(input).val(arr[1]);
            $(input).parent().find("input[name*=hidPer]").val(arr[0]);

        }

        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                $("#<%=txt_first.ClientID %>").val(r.st_name);
                $("#<%=firstid.ClientID %>").val(r.st_id);
            }
            if (id == "second") {
                $("#<%=txt_second.ClientID %>").val(r.st_name);
                $("#<%=secondid.ClientID %>").val(r.st_id);
            }
            if (id == "third") {
                $("#<%=txt_third.ClientID %>").val(r.st_name);
                $("#<%=thirdid.ClientID %>").val(r.st_id);
            }
            if (id == "fourth") {
                $("#<%=txt_fourth.ClientID %>").val(r.st_name);
                $("#<%=fourthid.ClientID %>").val(r.st_id);
            }
            if (id == "fifth") {
                $("#<%=txt_fifth.ClientID %>").val(r.st_name);
                $("#<%=fifthid.ClientID %>").val(r.st_id);
            }
            if (id == "sixth") {
                $("#<%=txt_sixth.ClientID %>").val(r.st_name);
                $("#<%=sixthid.ClientID %>").val(r.st_id);
            }

            $('#win').dialog('close');
        }
       

    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table style="width: 100%">
                    <tr>
                        <td align="left">
                            申请单号：
                            <asp:Label ID="LAB_MOVE_CODE" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="Btn_save_onclick" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Close" Text="关闭" OnClick="close" runat="server" />&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <cc1:TabContainer ID="TabContainer1" runat="server" Width="100%" TabStripPlacement="Top"
        ActiveTabIndex="0" AutoPostBack="false">
        <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="内部人员调动信息" TabIndex="0">
            <ContentTemplate>
                <div class="box-wrapper" style="text-align: center;">
                    <div class="box-outer">
                    <asp:Panel ID="Panel1" runat="server">
                        <table cellpadding="4" width="100%" cellspacing="1" style="background: #EEF7FD; white-space: nowrap;
                            text-align: left;" class="grid toptable" border="1">
                            <tr>
                                <td>
                                    调出部门：
                                    <asp:DropDownList ID="ddl_MOVE_OUTPART" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_OUTpartselectchanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    姓 名：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:DropDownList ID="ddlPer" runat="server" OnSelectedIndexChanged="ddlPer_selectchanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <div style="float: left;">
                                        内部调动类别：</div>
                                    <div>
                                        <asp:RadioButtonList ID="RBL_MOVE_TYPE" runat="server" Height="16px" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">调动</asp:ListItem>
                                            <asp:ListItem Value="1">借调</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    调入部门：
                                    <asp:DropDownList ID="ddl_MOVE_INPART" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_INpartselectchanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    拟从事岗位：
                                    <asp:DropDownList ID="ddl_MOVE_WORK" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    起始时间：
                                    <asp:TextBox runat="server" ID="TXT_MOVE_STARTTIME" class="easyui-datebox" editable="false"></asp:TextBox>
                                </td>
                                <td>
                                    拟结束时间：
                                    <asp:TextBox runat="server" ID="TXT_MOVE_ENDTIME" class="easyui-datebox" editable="false"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2">
                                    内部调动原因:
                                </td>
                                <td colspan="2" rowspan="2">
                                    <asp:TextBox ID="TXT_MOVE_REASON" runat="server" Width="800px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 100%; margin: 0 auto">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                                CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lbindex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
      
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:TemplateField HeaderText="固定资产编号">
                                        <ItemTemplate>
                                            <input id="GDZC_BIANHAO" name="name" runat="server" style="border-style: none; width: 150px"
                                                type="text" value='<%#Eval("BIANHAO") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="入库单号">
                                        <ItemTemplate>
                                            <input id="GDZC_CODE" name="name" runat="server" style="border-style: none; width: 150px"
                                                type="text" value='<%#Eval("INCODE") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="名称">
                                        <ItemTemplate>
                                            <input id="GDZC_NAME" name="name" runat="server" style="border-style: none; width: 450px"
                                                type="text" value='<%#Eval("NAME") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格或参数">
                                        <ItemTemplate>
                                            <input id="GDZC_MODEL" name="name" runat="server" style="border-style: none; width: 210px"
                                                type="text" value='<%#Eval("MODEL") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="是否转移">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlIsZY" runat="server" SelectedValue='<%#Eval("ISTRAN") %>'>
                                                <asp:ListItem Text="转移" Value="转移"></asp:ListItem>
                                                <asp:ListItem Text="不转移" Value="不转移"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="交接人">
                                        <ItemTemplate>
                                            <asp:TextBox ID="JJR" runat="server" Style="border-style: none; width: 80px" onchange="PerChange(this)"
                                                Text='<%#Eval("JERNM") %>'></asp:TextBox>
                                            <input id="hidPer" runat="server" type="hidden" name="hidPer" value='<%#Eval("JERID") %>' />
                                            <cc1:AutoCompleteExtender ID="name_AutoCompleteExtender" runat="server" CompletionSetCount="10"
                                                CompletionListCssClass="completionListElement" DelimiterCharacters="" Enabled="True"
                                                MinimumPrefixLength="1" ServiceMethod="get_st_id" ServicePath="~/Ajax.asmx" FirstRowSelected="true"
                                                TargetControlID="JJR" UseContextKey="True">
                                            </cc1:AutoCompleteExtender>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                    
                                      <asp:TemplateField HeaderText="放置地点">
                                        <ItemTemplate>
                                            <input id="GDZC_PLACE" name="name" runat="server" style="border-style: none; width: 210px"
                                                type="text" value='<%#Eval("PLACE") %>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="80px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        </asp:Panel>
                    </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="审 核" TabIndex="1">
            <ContentTemplate>
                <div class="box-inner">
                    <div class="box_right">
                        <div class="box-title">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        审核等级：
                                        <asp:DropDownList ID="ddl_authorize_rating" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_authorize_rating_selectchanged">
                                            <asp:ListItem Text="一级审批" Value="1" Selected="true"></asp:ListItem>
                                            <asp:ListItem Text="二级审批" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="三级审批" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="四级审批" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="五级审批" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="六级审批" Value="6"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="box-wrapper">
                    <div class="box-outer">
                        <table width="100%">
                            <tr>
                                <td style="font-size: large; text-align: center; height: 43px" colspan="2">
                                    人员调动审批表
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="box-outer">
                    <table align="center" width="100%" cellpadding="0" cellspacing="0" class="toptable grid"
                        border="1">
                        <asp:Panel ID="pal_depart_first" runat="server">
                            <tr id="tr1" runat="server">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="Type1">
                                        <asp:ListItem Text="调出部门" Value="调出部门"></asp:ListItem>
                                        <asp:ListItem Text="调入部门" Value="调入部门"></asp:ListItem>
                                        <asp:ListItem Text="主管经理" Value="主管经理"></asp:ListItem>
                                        <asp:ListItem Text="总经理" Value="总经理"></asp:ListItem>
                                        <asp:ListItem Text="综合办公室" Value="综合办公室"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_first" runat="server" Width="80px"></asp:TextBox>
                                                <input id="firstid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect1" runat="server" CssClass="hand" onClick="SelTechPersons1()">
                                                    <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择</asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfirst" RepeatColumns="2" runat="server" Enabled="false"
                                                    Height="20px">
                                                    <asp:ListItem Text="同意" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="N"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="first_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="first_opinion" Enabled="false" runat="server" TextMode="MultiLine"
                                                    Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pal_depart_second" runat="server">
                            <tr id="tr2" runat="server">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="Type2">
                                        <asp:ListItem Text="调出部门" Value="调出部门"></asp:ListItem>
                                        <asp:ListItem Text="调入部门" Value="调入部门"></asp:ListItem>
                                        <asp:ListItem Text="主管经理" Value="主管经理"></asp:ListItem>
                                        <asp:ListItem Text="总经理" Value="总经理"></asp:ListItem>
                                        <asp:ListItem Text="综合办公室" Value="综合办公室"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_second" runat="server" Width="80px"></asp:TextBox>
                                                <input id="secondid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect2" runat="server" CssClass="hand" onClick="SelTechPersons2()">
                                                    <asp:Image ID="AddImage2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择</asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsecond" RepeatColumns="2" Enabled="false" runat="server"
                                                    Height="20px">
                                                    <asp:ListItem Text="同意" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="N"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="second_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="second_opinion" runat="server" Enabled="false" TextMode="MultiLine"
                                                    Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pal_depart_third" runat="server">
                            <tr id="tr3" runat="server">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="Type3">
                                        <asp:ListItem Text="调出部门" Value="调出部门"></asp:ListItem>
                                        <asp:ListItem Text="调入部门" Value="调入部门"></asp:ListItem>
                                        <asp:ListItem Text="主管经理" Value="主管经理"></asp:ListItem>
                                        <asp:ListItem Text="总经理" Value="总经理"></asp:ListItem>
                                        <asp:ListItem Text="综合办公室" Value="综合办公室"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_third" runat="server" Width="80px"></asp:TextBox>
                                                <input id="thirdid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="hlSelect3" runat="server" CssClass="hand" onClick="SelTechPersons3()">
                                                    <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                                        align="absmiddle" runat="server" />
                                                    选择</asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblthird" RepeatColumns="2" Enabled="false" runat="server"
                                                    Height="20px">
                                                    <asp:ListItem Text="同意" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="N"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="third_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="third_opinion" runat="server" Enabled="false" TextMode="MultiLine"
                                                    Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pal_depart_fourth" runat="server">
                            <tr id="tr4" runat="server">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="Type4">
                                        <asp:ListItem Text="调出部门" Value="调出部门"></asp:ListItem>
                                        <asp:ListItem Text="调入部门" Value="调入部门"></asp:ListItem>
                                        <asp:ListItem Text="主管经理" Value="主管经理"></asp:ListItem>
                                        <asp:ListItem Text="总经理" Value="总经理"></asp:ListItem>
                                        <asp:ListItem Text="综合办公室" Value="综合办公室"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_fourth" runat="server" Width="80px"></asp:TextBox>
                                                <input id="fourthid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="hand" onClick="SelTechPersons4()">
                                                    <asp:Image ID="Image1" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择</asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfourth" RepeatColumns="2" Enabled="false" runat="server"
                                                    Height="20px">
                                                    <asp:ListItem Text="同意" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="N"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="fourth_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="fourth_opinion" runat="server" Enabled="false" TextMode="MultiLine"
                                                    Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pal_depart_fifth" runat="server">
                            <tr id="tr5" runat="server">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="Type5">
                                        <asp:ListItem Text="调出部门" Value="调出部门"></asp:ListItem>
                                        <asp:ListItem Text="调入部门" Value="调入部门"></asp:ListItem>
                                        <asp:ListItem Text="主管经理" Value="主管经理"></asp:ListItem>
                                        <asp:ListItem Text="总经理" Value="总经理"></asp:ListItem>
                                        <asp:ListItem Text="综合办公室" Value="综合办公室"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_fifth" runat="server" Width="80px"></asp:TextBox>
                                                <input id="fifthid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="hand" onClick="SelTechPersons5()">
                                                    <asp:Image ID="Image2" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择</asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblfifth" RepeatColumns="2" Enabled="false" runat="server"
                                                    Height="20px">
                                                    <asp:ListItem Text="同意" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="N"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="fifth_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="fifth_opinion" runat="server" Enabled="false" TextMode="MultiLine"
                                                    Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pal_depart_sixth" runat="server">
                            <tr id="tr6" runat="server">
                                <td align="center">
                                    <asp:DropDownList runat="server" ID="Type6">
                                        <asp:ListItem Text="调出部门" Value="调出部门"></asp:ListItem>
                                        <asp:ListItem Text="调入部门" Value="调入部门"></asp:ListItem>
                                        <asp:ListItem Text="主管经理" Value="主管经理"></asp:ListItem>
                                        <asp:ListItem Text="总经理" Value="总经理"></asp:ListItem>
                                        <asp:ListItem Text="综合办公室" Value="综合办公室"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td colspan="3">
                                    <table width="100%" cellpadding="4" cellspacing="1" border="1">
                                        <tr style="height: 25px">
                                            <td align="center" style="width: 10%">
                                                审批人
                                            </td>
                                            <td style="width: 20%">
                                                <asp:TextBox ID="txt_sixth" runat="server" Width="80px"></asp:TextBox>
                                                <input id="sixthid" type="text" runat="server" readonly="readonly" style="display: none" />
                                                <asp:HyperLink ID="HyperLink3" runat="server" CssClass="hand" onClick="SelTechPersons6()">
                                                    <asp:Image ID="Image3" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2" align="absmiddle"
                                                        runat="server" />
                                                    选择</asp:HyperLink>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核结论
                                            </td>
                                            <td align="center" style="width: 20%">
                                                <asp:RadioButtonList ID="rblsixth" RepeatColumns="2" Enabled="false" runat="server"
                                                    Height="20px">
                                                    <asp:ListItem Text="同意" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="不同意" Value="N"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="center" style="width: 10%">
                                                审核时间
                                            </td>
                                            <td style="width: 20%">
                                                <asp:Label ID="sixth_time" runat="server" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:TextBox ID="sixth_opinion" runat="server" Enabled="false" TextMode="MultiLine"
                                                    Width="100%" Height="42px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
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
</asp:Content>
