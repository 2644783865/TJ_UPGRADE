<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="YS_Cost_Budget_A_M_CON.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_A_M_CON" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    <asp:Label ID="LabelTitle" runat="server" Text="合同预算监控"></asp:Label>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
 
    <script type="text/javascript" language="javascript">
        var selectedColor = "#C0FF3E";
        var rowOverColor = "blue";
        var rowColor = "#EFF3FB";
        var selectedRows = new Object();

        function SelectRow(uniqueId, element) {
            if (typeof (selectedRows[uniqueId]) == "undefined")
                selectedRows[uniqueId] = false;
            selectedRows[uniqueId] = !selectedRows[uniqueId];
            element.style.backgroundColor = selectedRows[uniqueId] ? selectedColor : rowColor;
        }

        function ShowContract(id) {
            var autonum = Math.round(10000 * Math.random());
            window.open("../Contract_Data/CM_Contract_SW_Add.aspx?Action=View&autonum=" + autonum + "&condetail_id=" + id);
        } 

        function PurMarView(ContractNo, FatherCode) {
            var date = new Date();
            var nouse = date.getDate();
            var obj = new Object();
            sRet = window.showModalDialog("YS_Cost_Budget_A_M_Detail.aspx?ContractNo=" + ContractNo + "&FatherCode=" + FatherCode + "&time=" + nouse, obj, "dialogHeight:400px;dialogWidth:700px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
            if (sRet == "refresh") {

                window.location.href = window.location.href;
            }
        }

        function check(obj) {
            var data = obj.value;
            var re = /^[1-9]\d*$/;
            if (!re.test(data)) {
                alert('请输入正确的数据,应为正整数！');
            }
        }

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <tr>
                        <td> 
                            <%--<asp:RadioButtonList ID="rbl_profit" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick"
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="全部" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="毛利润赤字" Value="1"></asp:ListItem>
                                <asp:ListItem Text="毛利润非赤字" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>--%>
                        </td>
                        
                        <td>
                        合同号：<asp:TextBox ID="txt_search" runat="server" Text="" Width="150px">&nbsp;
                            </asp:TextBox><asp:Button ID="search1" runat="server" Text="查 询" OnClick="btn_search_OnClick" />
                        </td>
                        <td>
                        <asp:Button ID="btnShowTask" runat="server" Text="查看任务预算" OnClick="btn_ShowTask_OnClick"/>&nbsp&nbsp&nbsp
                        </td>
                        
                        <td align="center">
                            项目名称:
                            <asp:DropDownList ID="ddl_project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 工程名称:
                            <asp:DropDownList ID="ddl_engineer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <%--<td align="right">
                        <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_OnClick">
                                <asp:Image ID="ModImahe" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />费用结算</asp:LinkButton>
                            <asp:Button ID="btn_daochu" runat="server" Text="导出Excel" OnClick="btn_daochu_Click" Visible="false"/>&nbsp;
                        </td>--%>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="100%">
                    <%--<tr>
                        <td style="width: 250px">
                            
                            <asp:Button ID="btnShowPopup" runat="server" Text="更多筛选" />
                            <asp:ModalPopupExtender ID="ModalPopupExtenderSearch" runat="server" TargetControlID="btnShowPopup"
                                PopupControlID="Pal_condition" Drag="false" Enabled="True" DynamicServicePath=""
                                Y="80">                            
                            </asp:ModalPopupExtender>
                            </td>
                            <td align="left">
                            
                            <asp:Button ID="btnShowSta" runat="server" Text="查看成本统计信息" OnClick="btn_ShowSta_OnClick"/>
                            </td>
                            
                        
                    </tr>--%>
                </table>
                <%--<asp:Panel ID="Pal_condition" runat="server" UpdateMode="Conditional">
                    <table width="100%" style="background-color: #CCCCFF; border: solid 1px black;">
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="QueryButton" runat="server" Text="查询" OnClick="btn_search_OnClick" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnClose" runat="server" Text="取消" OnClick="btnClose_Click" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                                &nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                生产制号：<asp:TextBox ID="txtprotect_num" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                外协费用：<asp:DropDownList ID="dpl_waixie" runat="server">
                                    <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="小于90%" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="达到90%（黄字）" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="超出预算（红字）" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                材料费用：<asp:DropDownList ID="dpl_materials" runat="server">
                                    <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="小于90%" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="达到90%（黄字）" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="超出预算（红字）" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                人工费用：<asp:DropDownList ID="dpl_labor" runat="server">
                                    <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="小于90%" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="达到90%（黄字）" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="超出预算（红字）" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                           <td>
                                其它费用：<asp:DropDownList ID="dpl_other" runat="server">
                                    <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="小于90%" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="达到90%（黄字）" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="超出预算（红字）" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                毛 利 润 ：<asp:DropDownList ID="dpl_all_profit" runat="server">
                                    <asp:ListItem Text="--请选择--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="达到预算利润" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="达到预算利润90%" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="小于预算利润90%" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                制单人员：<asp:DropDownList ID="dpl_people" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                提交预算时间：<asp:TextBox ID="txt_make_sta" runat="server" Width="90px" onchange="dateCheck(this)"></asp:TextBox>至&nbsp;<asp:TextBox ID="txt_make_end" runat="server" Width="90px" onchange="dateCheck(this)"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                    TodaysDateFormat="yyyy-MM-dd" TargetControlID="txt_make_sta">
                                </asp:CalendarExtender>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd" DaysModeTitleFormat="MM月,yyyy年"
                                    TodaysDateFormat="yyyy-MM-dd" TargetControlID="txt_make_end">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                合同完结时间：<asp:TextBox ID="finish_sta_time" runat="server" Width="90px" onchange="dateCheck(this)"></asp:TextBox>至&nbsp;<asp:TextBox ID="finish_end_time" runat="server" Width="90px" onchange="dateCheck(this)"></asp:TextBox><asp:CalendarExtender ID="finish_calender_sta" runat="server" Format="yyyy-MM-dd"
                                    DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy-MM-dd" TargetControlID="finish_sta_time">
                                </asp:CalendarExtender>
                                <asp:CalendarExtender ID="finish_calender_end" runat="server" Format="yyyy-MM-dd"
                                    DaysModeTitleFormat="MM月,yyyy年" TodaysDateFormat="yyyy-MM-dd" TargetControlID="finish_end_time">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>--%>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto; overflow-x: yes; overflow-y: hidden;">
                <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid nowrap" runat="server"
                    AutoGenerateColumns="False" CellPadding="1" ForeColor="#333333" OnRowDataBound="GridView1_onrowdatabound">
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" HeaderText="序号"
                            ItemStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                <asp:HiddenField ID="hdfMP_ID" runat="server" Value='<%# Eval("YS_CONTRACT_NO") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="合同号" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_YS_CONTRACT_NO" runat="server" Text='<%#Eval("YS_CONTRACT_NO") %>'
                                    ToolTip="双击关联合同信息！"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="项目名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_pjname" runat="server" Text='<%#Eval("PCON_PJNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="工程名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_engname" runat="server" Text='<%#Eval("PCON_ENGNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="YS_BUDGET_INCOME" HeaderText="预算收入" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_Cost" HeaderText="预算费用合计" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:BoundField DataField="YS_RealCost" HeaderText="实际费用合计" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}" />
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="毛利润(预)" HeaderStyle-ForeColor="Red"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_PROFIT" runat="Server" Value='<%#Eval("YS_PROFIT_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_PROFIT" runat="Server" Text='<%#Eval("YS_PROFIT_BG","{0:N2}") %>' />
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth_YS_AMOUNT(Eval("YS_PROFIT_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="黑色金属(预)" HeaderStyle-ForeColor="Brown"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_FERROUS_METAL" runat="Server" Value='<%#Eval("YS_FERROUS_METAL_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_FERROUS_METAL" runat="Server" Text='<%#Eval("YS_FERROUS_METAL_BG","{0:N2}") %>' />
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_FERROUS_METAL_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="外购件(预)" HeaderStyle-ForeColor="Brown"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_PURCHASE_PART" runat="Server" Value='<%#Eval("YS_PURCHASE_PART_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_PURCHASE_PART" runat="Server" Text='<%#Eval("YS_PURCHASE_PART_BG","{0:N2}") %>' />
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_PURCHASE_PART_BG_percent").ToString()) %>"
                                        height="9" id="ID_IMG" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="铸锻件(预)" HeaderStyle-ForeColor="Brown"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_CASTING_FORGING" runat="Server" Value='<%#Eval("YS_CASTING_FORGING_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_CASTING_FORGING" runat="server" Text='<%#Eval("YS_CASTING_FORGING_COST_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_CASTING_FORGING_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="油漆涂料(预)" HeaderStyle-ForeColor="Brown"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_PAINT_COATING" runat="Server" Value='<%#Eval("YS_PAINT_COATING_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_PAINT_COATING" runat="server" Text='<%#Eval("YS_PAINT_COATING_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_PAINT_COATING_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="电气电料(预)" HeaderStyle-ForeColor="Brown"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_ELECTRICAL" runat="Server" Value='<%#Eval("YS_ELECTRICAL_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_ELECTRICAL" runat="server" Text='<%#Eval("YS_ELECTRICAL_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_ELECTRICAL_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="其它材料费(预)" HeaderStyle-ForeColor="Brown"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_OTHERMAT_COST" runat="Server" Value='<%#Eval("YS_OTHERMAT_COST_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_OTHERMAT_COST" runat="server" Text='<%#Eval("YS_OTHERMAT_COST_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_OTHERMAT_COST_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="材料费合计(预)" HeaderStyle-ForeColor="Green"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_MATERIAL_COST" runat="Server" Value='<%#Eval("YS_MATERIAL_COST_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_MATERIAL_COST" runat="server" Text='<%#Eval("YS_MATERIAL_COST_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_MATERIAL_COST_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="直接人工(预)" HeaderStyle-ForeColor="Green"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_TEAM_CONTRACT" runat="Server" Value='<%#Eval("YS_TEAM_CONTRACT_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_TEAM_CONTRACT" runat="server" Text='<%#Eval("YS_TEAM_CONTRACT_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_TEAM_CONTRACT_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="厂内分包(预)" HeaderStyle-ForeColor="Green"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_FAC_CONTRACT" runat="Server" Value='<%#Eval("YS_FAC_CONTRACT_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_FAC_CONTRACT" runat="server" Text='<%#Eval("YS_FAC_CONTRACT_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_FAC_CONTRACT_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="生产外协(预)" HeaderStyle-ForeColor="Green"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_PRODUCT_OUT" runat="Server" Value='<%#Eval("YS_PRODUCT_OUT_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_PRODUCT_OUT" runat="server" Text='<%#Eval("YS_PRODUCT_OUT_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_PRODUCT_OUT_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="运费(预)" HeaderStyle-ForeColor="Green"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <div style="position: static; width: 100px;">
                                    <asp:HiddenField ID="hidden_TRANS_COST" runat="Server" Value='<%#Eval("YS_TRANS_COST_BG_hide_percent","{0:N2}") %>' />
                                    <div style="width: 100px; position: static; z-index: 100; top: 4px; text-align: left;">
                                        <asp:Label ID="lab_TRANS_COST" runat="server" Text='<%#Eval("YS_TRANS_COST_BG","{0:N2}") %>'></asp:Label>
                                    </div>
                                    <img alt="" src="/YS_Data/update1.jpg" width="<%#GetIMGWidth(Eval("YS_TRANS_COST_BG_percent").ToString()) %>"
                                        height="9" /><br />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="毛利润(实)" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="Red">
                            <ItemTemplate>
                                <asp:Label ID="lab_PROFIT_R" runat="server" Text='<%#Eval("YS_PROFIT","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="黑色金属(实)" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="Brown">
                            <ItemTemplate>
                                <asp:Label ID="lab_FERROUS_METAL_R" runat="server" Text='<%#Eval("YS_FERROUS_METAL","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="外购件(实)" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="Brown">
                            <ItemTemplate>
                                <asp:Label ID="lab_PURCHASE_PART_R" runat="server" Text='<%#Eval("YS_PURCHASE_PART","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="铸锻件(实)" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="Brown">
                            <ItemTemplate>
                                <asp:Label ID="lab_CASTING_FORGING_R" runat="server" Text='<%#Eval("YS_CASTING_FORGING","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="油漆涂料(实)" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="Brown">
                            <ItemTemplate>
                                <asp:Label ID="lab_PAINT_COATING_R" runat="server" Text='<%#Eval("YS_PAINT_COATING","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="电气电料(实)" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="Brown">
                            <ItemTemplate>
                                <asp:Label ID="lab_ELECTRICAL_R" runat="server" Text='<%#Eval("YS_ELECTRICAL","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="其他材料费(实)" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="Brown">
                            <ItemTemplate>
                                <asp:Label ID="lab_OTHERMAT_COST_R" runat="server" Text='<%#Eval("YS_OTHERMAT_COST","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="材料费合计(实)" HeaderStyle-Wrap="false"
                        HeaderStyle-ForeColor="Green">
                            <ItemTemplate>
                                <asp:Label ID="lab_MATERIAL_COST_R" runat="server" Text='<%#Eval("YS_MATERIAL_COST","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="直接人工(实)" HeaderStyle-Wrap="false"
                         HeaderStyle-ForeColor="Green">
                            <ItemTemplate>
                                <asp:Label ID="lab_TEAM_CONTRACT_R" runat="server" Text='<%#Eval("YS_TEAM_CONTRACT","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="厂内分包(实)" HeaderStyle-Wrap="false"
                         HeaderStyle-ForeColor="Green">
                            <ItemTemplate>
                                <asp:Label ID="lab_FAC_CONTRACT_R" runat="server" Text='<%#Eval("YS_FAC_CONTRACT","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="生产外协(实)" HeaderStyle-Wrap="false"
                         HeaderStyle-ForeColor="Green">
                            <ItemTemplate>
                                <asp:Label ID="lab_PRODUCT_OUT_R" runat="server" Text='<%#Eval("YS_PRODUCT_OUT","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                         <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="运费(实)" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="Green">
                            <ItemTemplate>
                                <asp:Label ID="lab_TRANS_COST_R" runat="server" Text='<%#Eval("YS_TRANS_COST","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="YS_Finshtime" HeaderText="入库时间" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" />

                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                </br>
            </div>
            <asp:Panel ID="NoDataPanel" runat="server">
                没有记录!</asp:Panel>
            <uc1:UCPaging ID="UCPaging1" runat="server" />
        </div>
    </div>
    <div>
        <table border="0" cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td style="width: 15" align="Left">
                    <img alt="" src="/YS_Data/update1.jpg" width="50px" height="15" />
                    <asp:Label ID="labe1" runat="Server" Text="表示订单完成百分比" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp;
                    <img alt="" src="/YS_Data/LightSalmon.jpg" width="50px" height="15" />
                    <asp:Label ID="Label3" runat="Server" Text="表示订单达到预算90%" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp;
                    <img alt="" src="/YS_Data/Yellow.jpg" width="50px" height="15" />
                    <asp:Label ID="Label2" runat="Server" Text="表示订单超过预算" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
