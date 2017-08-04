<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.master"
    CodeBehind="YS_Cost_Budget_View.aspx.cs" Inherits="ZCZJ_DPF.YS_Data.YS_Cost_Budget_View" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    预算编制
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="left">
                            任务号:
                            <asp:TextBox ID="txt_search" runat="server" Width="200px"></asp:TextBox><asp:Button
                                ID="btn_search" runat="server" Text="查询" OnClick="btn_search_OnClick" />
                        </td>
                        <td align="center">
                            制单人：<asp:DropDownList ID="ddl_addper" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="center">
                            审核状态：<asp:DropDownList ID="ddl_State" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:CheckBox ID="ckb_time" runat="server" AutoPostBack="true" Text="超期未完成编制" OnCheckedChanged="btn_search_OnClick" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
                <table width="98%">
                    <tr>
                        <td align="left">
                            项目名称:
                            <asp:DropDownList ID="ddl_project" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_project_OnSelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 设备名称:
                            <asp:DropDownList ID="ddl_engineer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="btn_search_OnClick">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            
                            <asp:LinkButton ID="btnModify" runat="server" OnClick="btnModify_OnClick">
                                <asp:Image ID="ModImahe" ImageUrl="~/Assets/images/res.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                修改预算</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="btnAddMar" runat="server" OnClick="btnAddMar_OnClick">
                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/icons/pcadd.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                新增预算</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDelete" runat="server" Text="删除" Font-Size="Small" OnClick="btnDelete_OnClick"
                                OnClientClick="return confirm('删除后不可恢复，确认删除吗？');" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="width: 100%; overflow: auto;">
                <asp:GridView ID="GridView1" Width="111%" CssClass="toptable grid nowrap" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <%--序号--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="序号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                <asp:HiddenField ID="hdfMP_ID" runat="server" Value='<%# Eval("YS_CONTRACT_NO") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--任务号--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="任务号" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TSAID" runat="server" Text='<%#Eval("YS_TSA_ID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--合同号--%>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="合同号" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_YS_CONTRACT_NO" runat="server" Text='<%#Eval("YS_CONTRACT_NO") %>'
                                    ToolTip="双击关联合同信息！"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--项目名称--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="项目名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pcon_pjname" runat="server" Text='<%#Eval("CM_PROJ") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--设备名称--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="设备名称" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TSA_ENGNAME" runat="server" Text='<%#Eval("TSA_ENGNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <%--预算收入--%>
                        <asp:BoundField DataField="YS_BUDGET_INCOME" ItemStyle-HorizontalAlign="Center" HeaderText="预算收入"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--预算总额--%>
                        <asp:BoundField DataField="YS_TOTALCOST_ALL" ItemStyle-HorizontalAlign="Center" HeaderText="预算总额"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--毛利润--%>
                        <asp:BoundField DataField="YS_PROFIT" ItemStyle-HorizontalAlign="Center" HeaderText="毛利润"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--毛利率--%>
                        <asp:BoundField DataField="YS_PROFIT_RATE" ItemStyle-HorizontalAlign="Center" HeaderText="毛利率"
                            HeaderStyle-Wrap="false" DataFormatString="{0:P}">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--黑色金属--%>
                        <asp:BoundField DataField="YS_FERROUS_METAL" ItemStyle-HorizontalAlign="Center" HeaderText="黑色金属"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False" ForeColor="Brown"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--外购件--%>
                        <asp:BoundField DataField="YS_PURCHASE_PART" ItemStyle-HorizontalAlign="Center" HeaderText="外购件"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False" ForeColor="Brown"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--加工件--%>
                        <asp:BoundField DataField="YS_MACHINING_PART" ItemStyle-HorizontalAlign="Center"
                            HeaderText="加工件" HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False" ForeColor="Brown"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--油漆涂料--%>
                        <asp:BoundField DataField="YS_PAINT_COATING" ItemStyle-HorizontalAlign="Center" HeaderText="油漆涂料"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False" ForeColor="Brown"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--电气电料--%>
                        <asp:BoundField DataField="YS_ELECTRICAL" ItemStyle-HorizontalAlign="Center" HeaderText="电气电料"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False" ForeColor="Brown"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--其它材料费--%>
                        <asp:BoundField DataField="YS_OTHERMAT_COST" ItemStyle-HorizontalAlign="Center" HeaderText="其它材料费"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Brown" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False" ForeColor="Brown"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--班组承包--%>
                        <asp:BoundField DataField="YS_TEAM_CONTRACT" ItemStyle-HorizontalAlign="Center" HeaderText="直接人工"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--厂内分包--%>
                        <asp:BoundField DataField="YS_FAC_CONTRACT" ItemStyle-HorizontalAlign="Center" HeaderText="厂内分包"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--生产外协--%>
                        <asp:BoundField DataField="YS_PRODUCT_OUT" ItemStyle-HorizontalAlign="Center" HeaderText="生产外协"
                            HeaderStyle-Wrap="false" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--运费--%>
                        <asp:BoundField DataField="YS_TRANS_COST" ItemStyle-HorizontalAlign="Center" HeaderText="运费"
                            HeaderStyle-Wrap="false" HeaderStyle-ForeColor="Green" DataFormatString="{0:N2}">
                            <HeaderStyle Wrap="False" ForeColor="Green"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                       
                        
                        <%--制单人--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="制单人" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_addper" runat="server" Text='<%#Eval("YS_ADDNAME") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <%--制单时间--%>
                        <asp:BoundField DataField="YS_ADDTIME" HeaderText="制单时间" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--制单完成期限--%>
                        <asp:BoundField DataField="YS_ADDFINISHTIME" HeaderText="制单完成期限" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <%--进度--%>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="进度" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lab_State" runat="server" Text='<%# Eval("YS_STATE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <%--备注--%>
                        <asp:BoundField DataField="YS_NOTE" HeaderText="备注" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false">
                            <HeaderStyle Wrap="False"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
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
                    <img alt="" src="/YS_Data/Sienna.jpg" width="50px" height="15" />
                    <asp:Label ID="Label3" runat="Server" Text="表示部门审核完毕，未提交领导审核" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp;
                    <img alt="" src="/YS_Data/Yellow.jpg" width="50px" height="15" />
                    <asp:Label ID="Label2" runat="Server" Text="表示需要完善" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    <img alt="" src="/YS_Data/red.jpg" width="50px" height="15" />
                    <asp:Label ID="Label1" runat="Server" Text="表示领导审核驳回" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    <img alt="" src="/YS_Data/pink.jpg" width="50px" height="15" />
                    <asp:Label ID="Label4" runat="Server" Text="审核通过，可重新发起预算" Font-Size="Small" ForeColor="#000066"
                        Font-Bold="True" />
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>
    

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

        function PurMarEdit(YS_CONTRACT_NO, DEP, INDEX) {
            var autonum = Math.round(10000 * Math.random());
            window.open("YS_Cost_Budget_Add_Detail.aspx?action=Edit&nameemail=null&autonum=" + autonum + "&YS_CONTRACT_NO=" + YS_CONTRACT_NO + "&DEP=" + DEP + "&INDEX=" + INDEX);
        }

        function PurMarView(YS_CONTRACT_NO, INDEX) {
            var autonum = Math.round(10000 * Math.random());
            window.open("YS_Cost_Budget_Add_Detail.aspx?action=View&autonum=" + autonum + "&YS_CONTRACT_NO=" + YS_CONTRACT_NO + "&DEP=null&INDEX=" + INDEX);
        }

        function PurMarAdd()//增加预算
        {
            var date = new Date();
            var nouse = date.getDate();
            var obj = new Object();
            sRet = window.showModalDialog("YS_Cost_Budget_Add.aspx?action=Add&time=" + nouse, obj, "dialogHeight:700px;dialogWidth:750px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
            if (sRet == "refresh") {

                window.location.href = window.location.href;
            }
        }

        function PurMarEdit_AddPer(YS_CONTRACT_NO)//修改预算
        {
            var date = new Date();
            var nouse = date.getDate();
            var obj = new Object();
            sRet = window.showModalDialog("YS_Cost_Budget_Add.aspx?action=Edit&YS_CONTRACT_NO=" + YS_CONTRACT_NO + "&time=" + nouse, obj, "dialogHeight:700px;dialogWidth:750px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
            if (sRet == "refresh") {
                window.location.href = window.location.href;
            }
        }

        function PurView(ContractNo) {
            var date = new Date();
            var nouse = date.getDate();
            var obj = new Object();
            sRet = window.showModalDialog("YS_Cost_Budget_Add_Original.aspx?ContractNo=" + ContractNo + "&time=" + nouse, obj, "dialogHeight:700px;dialogWidth:750px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
            if (sRet == "refresh") {
                window.location.href = window.location.href;
            }
        }

        function PurImport(ContractNo) {
            var date = new Date();
            var nouse = date.getDate();
            var obj = new Object();
            sRet = window.showModalDialog("YS_Cost_Budget_Add_Detail_Import.aspx?ContractNo=" + ContractNo + "&time=" + nouse, obj, "dialogHeight:300px;dialogWidth:600px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");
            if (sRet == "refresh") {
                window.location.href = window.location.href;
            }
        }

        function View_Attachment(ContractNo) {
            var date = new Date();
            var nouse = date.getDate();
            var obj = new Object();
            sRet = window.open("YS_Attachment.aspx?ContractNo=" + ContractNo);
            if (sRet == "refresh") {
                window.location.href = window.location.href;
            }
        } 
    </script>

</asp:Content>
