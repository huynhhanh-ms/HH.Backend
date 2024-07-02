using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PI.Domain.Dto.Medicine
{


    public class MedicineInfoSystemResponse
    {
        [JsonProperty("result")]
        public MedicineResultResponse Result { get; set; }
    }
    public class MedicineResultResponse
    {
        public int TotalCount { get; set; }
        public List<Item> Items { get; set; } = new List<Item>();
    }
    
    public class Item
    {
        public object HoatChatHamLuong { get; set; }
        public object DangBaoChe { get; set; }
        public object DongGoi { get; set; }
        public object TieuChuan { get; set; }
        public object TieuChuanId { get; set; }
        public object TuoiTho { get; set; }
        public string TenCongTyDangKy { get; set; }
        public string NuocDangKyId { get; set; }
        public string NuocDangKy { get; set; }
        public string DiaChiDangKy { get; set; }
        public object TenCongTySanXuat { get; set; }
        public object NuocSanXuat { get; set; }
        public object DiaChiSanXuat { get; set; }
        public object NuocSanXuatId { get; set; }
        public object DotCap { get; set; }
        public object NgayCapSoDangKy { get; set; }
        public object SoQuyetDinh { get; set; }
        public string HoatChatChinh { get; set; }
        public object MessageError { get; set; }
        public object IsCapNhatNHHSDK { get; set; }
        public object TenHoatChatChinh { get; set; }
        public object HoatChatDangKy { get; set; }
        public object HamLuong { get; set; }
        public object HangSanXuat { get; set; }
        public object DonViTinh { get; set; }
        public object GiaDangKy { get; set; }
        public object TenKhongDau { get; set; }
        public object CoSoSanXuat { get; set; }
        public object KetQua { get; set; }
        public object IsLuongCapNhat { get; set; }
        public object ListNoiDungThayDoi { get; set; }
        public bool IsHetHan { get; set; }
        public object MaThuoc { get; set; }
        public string TenThuoc { get; set; }
        public string SoDangKy { get; set; }
        public string SoDangKyCu { get; set; }
        public ThongTinDangKyThuoc ThongTinDangKyThuoc { get; set; }
        public ThongTinRutSoDangKy ThongTinRutSoDangKy { get; set; }
        public ThongTinThuocCoBan ThongTinThuocCoBan { get; set; }
        public ThongTinTaiLieu ThongTinTaiLieu { get; set; }
        public ThuocKiemSoatDacBiet ThuocKiemSoatDacBiet { get; set; }
        public VacXinSinhPham VacXinSinhPham { get; set; }
        public CongTySanXuat CongTySanXuat { get; set; }
        public CongTyDangKy CongTyDangKy { get; set; }
        public int PhanLoaiThuocEnum { get; set; }
        public object GhiChu { get; set; }
        public bool IsActive { get; set; }
        public object NguonDuLieuEnum { get; set; }
        public object TrangThai { get; set; }
        public object LyDoSuaDoi { get; set; }
        public object DoanhNghiepId { get; set; }
        public object MstDoanhNghiep { get; set; }
        public object IsXacNhanSoHuuDoanhNghiep { get; set; }
        public object IsDaRutSoDangKy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ThongTinDangKyThuoc
    {
        [JsonProperty("ngayCapSoDangKy")]
        [Column(TypeName = "datetime")]
        public DateTime NgayCapSoDangKy { get; set; }

        [JsonProperty("ngayGiaHanSoDangKy")]
        [Column(TypeName = "datetime")]
        public DateTime? NgayGiaHanSoDangKy { get; set; }

        [JsonProperty("ngayHetHanSoDangKy")]
        [Column(TypeName = "datetime")]
        public DateTime NgayHetHanSoDangKy { get; set; }

        [JsonProperty("soQuyetDinh")]
        [StringLength(255)]
        public string SoQuyetDinh { get; set; } = null!;

        [JsonProperty("urlSoQuyetDinh")]
        [StringLength(255)]
        public string? UrlSoQuyetDinh { get; set; }

        [JsonProperty("dotCap")] public string DotCap { get; set; }

        [JsonProperty("isCapNhatNHHSDK")] public bool? IsCapNhatNHHSDK { get; set; }
    }

    public class ThongTinRutSoDangKy
    {
        [JsonProperty("ngayRutSoDangKy")] public string? UrlCongVanRutSoDangKy { get; set; }
    }

    public class ThongTinThuocCoBan
    {
        [JsonProperty("hoatChatHamLuong")] public string? HoatChatHamLuong { get; set; }
        [JsonProperty("hoatChatChinh")] public string HoatChatChinh { get; set; }
        [JsonProperty("hoatChatChinhId")] public string? HoatChatChinhId { get; set; }
        [JsonProperty("hamLuong")] public string HamLuong { get; set; }
        [JsonProperty("dangBaoChe")] public string DangBaoChe { get; set; }
        [JsonProperty("dangBaoCheId")] public string? DangBaoCheId { get; set; }
        [JsonProperty("dongGoi")] public string DongGoi { get; set; }
        [JsonProperty("dongGoiJson")] public string? DongGoiJson { get; set; }
        [JsonProperty("maDuongDung")] public string? MaDuongDung { get; set; }
        [JsonProperty("tenDuongDung")] public string? TenDuongDung { get; set; }
        [JsonProperty("tieuChuan")] public string TieuChuan { get; set; }
        [JsonProperty("tenDuongDungId")] public string? TieuChuanId { get; set; }
        public string TuoiTho { get; set; }
        public string? LoaiThuoc { get; set; }
        public string? LoaiThuocId { get; set; }
        public string? NhomThuoc { get; set; }
        public string? NhomThuocId { get; set; }
    }

    public class ThongTinTaiLieu
    {
        public string? UrlHuongDanSuDung { get; set; }
        public string? UrlNhan { get; set; }
        public string? UrlNhanVaHDSD { get; set; }
    }

    public class ThuocKiemSoatDacBiet
    {
        public bool? IsHoSoACTD { get; set; }
        public bool? IsHoSoLamSang { get; set; }
        public string? NguoiLap { get; set; }
        public string? ChuTichHoiDong { get; set; }
        public string? ThuKyHoiDong { get; set; }
        public string? NguoiDuyet { get; set; }
        public DateTime? NgayQuyetDinhCongVan { get; set; }
        public string? SoQuyetDinhCongVan { get; set; }
    }

    public class VacXinSinhPham
    {
        public int LoaiVacXin { get; set; }
        public string PhongBenh { get; set; }
    }

    public class CongTySanXuat
    {
        public string TenCongTySanXuat { get; set; }
        public string DiaChiSanXuat { get; set; }
        public string NuocSanXuat { get; set; }
        public string? NuocSanXuatId { get; set; }
    }

    public class CongTyDangKy
    {
        public string TenCongTyDangKy { get; set; }
        public string DiaChiCongTyDangKy { get; set; }
        public string NuocDangKy { get; set; }
        public string? NuocDangKyId { get; set; }
    }
}