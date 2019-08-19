﻿// WARNING: Please don't edit this file. It was generated by C++/WinRT v1.0.180821.2

#pragma once
#include "winrt/impl/Windows.Perception.Spatial.1.h"
#include "winrt/impl/Microsoft.Azure.SpatialAnchors.1.h"

WINRT_EXPORT namespace winrt::Microsoft::Azure::SpatialAnchors {

struct AnchorLocatedDelegate : Windows::Foundation::IUnknown
{
    AnchorLocatedDelegate(std::nullptr_t = nullptr) noexcept {}
    template <typename L> AnchorLocatedDelegate(L lambda);
    template <typename F> AnchorLocatedDelegate(F* function);
    template <typename O, typename M> AnchorLocatedDelegate(O* object, M method);
    template <typename O, typename M> AnchorLocatedDelegate(com_ptr<O>&& object, M method);
    template <typename O, typename M> AnchorLocatedDelegate(weak_ref<O>&& object, M method);
    void operator()(Windows::Foundation::IInspectable const& sender, Microsoft::Azure::SpatialAnchors::AnchorLocatedEventArgs const& args) const;
};

struct LocateAnchorsCompletedDelegate : Windows::Foundation::IUnknown
{
    LocateAnchorsCompletedDelegate(std::nullptr_t = nullptr) noexcept {}
    template <typename L> LocateAnchorsCompletedDelegate(L lambda);
    template <typename F> LocateAnchorsCompletedDelegate(F* function);
    template <typename O, typename M> LocateAnchorsCompletedDelegate(O* object, M method);
    template <typename O, typename M> LocateAnchorsCompletedDelegate(com_ptr<O>&& object, M method);
    template <typename O, typename M> LocateAnchorsCompletedDelegate(weak_ref<O>&& object, M method);
    void operator()(Windows::Foundation::IInspectable const& sender, Microsoft::Azure::SpatialAnchors::LocateAnchorsCompletedEventArgs const& args) const;
};

struct OnLogDebugDelegate : Windows::Foundation::IUnknown
{
    OnLogDebugDelegate(std::nullptr_t = nullptr) noexcept {}
    template <typename L> OnLogDebugDelegate(L lambda);
    template <typename F> OnLogDebugDelegate(F* function);
    template <typename O, typename M> OnLogDebugDelegate(O* object, M method);
    template <typename O, typename M> OnLogDebugDelegate(com_ptr<O>&& object, M method);
    template <typename O, typename M> OnLogDebugDelegate(weak_ref<O>&& object, M method);
    void operator()(Windows::Foundation::IInspectable const& sender, Microsoft::Azure::SpatialAnchors::OnLogDebugEventArgs const& args) const;
};

struct SessionErrorDelegate : Windows::Foundation::IUnknown
{
    SessionErrorDelegate(std::nullptr_t = nullptr) noexcept {}
    template <typename L> SessionErrorDelegate(L lambda);
    template <typename F> SessionErrorDelegate(F* function);
    template <typename O, typename M> SessionErrorDelegate(O* object, M method);
    template <typename O, typename M> SessionErrorDelegate(com_ptr<O>&& object, M method);
    template <typename O, typename M> SessionErrorDelegate(weak_ref<O>&& object, M method);
    void operator()(Windows::Foundation::IInspectable const& sender, Microsoft::Azure::SpatialAnchors::SessionErrorEventArgs const& args) const;
};

struct SessionUpdatedDelegate : Windows::Foundation::IUnknown
{
    SessionUpdatedDelegate(std::nullptr_t = nullptr) noexcept {}
    template <typename L> SessionUpdatedDelegate(L lambda);
    template <typename F> SessionUpdatedDelegate(F* function);
    template <typename O, typename M> SessionUpdatedDelegate(O* object, M method);
    template <typename O, typename M> SessionUpdatedDelegate(com_ptr<O>&& object, M method);
    template <typename O, typename M> SessionUpdatedDelegate(weak_ref<O>&& object, M method);
    void operator()(Windows::Foundation::IInspectable const& sender, Microsoft::Azure::SpatialAnchors::SessionUpdatedEventArgs const& args) const;
};

struct TokenRequiredDelegate : Windows::Foundation::IUnknown
{
    TokenRequiredDelegate(std::nullptr_t = nullptr) noexcept {}
    template <typename L> TokenRequiredDelegate(L lambda);
    template <typename F> TokenRequiredDelegate(F* function);
    template <typename O, typename M> TokenRequiredDelegate(O* object, M method);
    template <typename O, typename M> TokenRequiredDelegate(com_ptr<O>&& object, M method);
    template <typename O, typename M> TokenRequiredDelegate(weak_ref<O>&& object, M method);
    void operator()(Windows::Foundation::IInspectable const& sender, Microsoft::Azure::SpatialAnchors::TokenRequiredEventArgs const& args) const;
};

}

namespace winrt::impl {

}

WINRT_EXPORT namespace winrt::Microsoft::Azure::SpatialAnchors {

struct WINRT_EBO AnchorLocateCriteria :
    Microsoft::Azure::SpatialAnchors::IAnchorLocateCriteria
{
    AnchorLocateCriteria(std::nullptr_t) noexcept {}
};

struct WINRT_EBO AnchorLocatedEventArgs :
    Microsoft::Azure::SpatialAnchors::IAnchorLocatedEventArgs
{
    AnchorLocatedEventArgs(std::nullptr_t) noexcept {}
};

struct WINRT_EBO CloudSpatialAnchor :
    Microsoft::Azure::SpatialAnchors::ICloudSpatialAnchor
{
    CloudSpatialAnchor(std::nullptr_t) noexcept {}
};

struct WINRT_EBO CloudSpatialAnchorSession :
    Microsoft::Azure::SpatialAnchors::ICloudSpatialAnchorSession
{
    CloudSpatialAnchorSession(std::nullptr_t) noexcept {}
};

struct WINRT_EBO CloudSpatialAnchorSessionDeferral :
    Microsoft::Azure::SpatialAnchors::ICloudSpatialAnchorSessionDeferral
{
    CloudSpatialAnchorSessionDeferral(std::nullptr_t) noexcept {}
};

struct WINRT_EBO CloudSpatialAnchorSessionDiagnostics :
    Microsoft::Azure::SpatialAnchors::ICloudSpatialAnchorSessionDiagnostics
{
    CloudSpatialAnchorSessionDiagnostics(std::nullptr_t) noexcept {}
};

struct WINRT_EBO CloudSpatialAnchorWatcher :
    Microsoft::Azure::SpatialAnchors::ICloudSpatialAnchorWatcher
{
    CloudSpatialAnchorWatcher(std::nullptr_t) noexcept {}
};

struct WINRT_EBO LocateAnchorsCompletedEventArgs :
    Microsoft::Azure::SpatialAnchors::ILocateAnchorsCompletedEventArgs
{
    LocateAnchorsCompletedEventArgs(std::nullptr_t) noexcept {}
};

struct WINRT_EBO NearAnchorCriteria :
    Microsoft::Azure::SpatialAnchors::INearAnchorCriteria
{
    NearAnchorCriteria(std::nullptr_t) noexcept {}
};

struct WINRT_EBO OnLogDebugEventArgs :
    Microsoft::Azure::SpatialAnchors::IOnLogDebugEventArgs
{
    OnLogDebugEventArgs(std::nullptr_t) noexcept {}
};

struct WINRT_EBO SessionConfiguration :
    Microsoft::Azure::SpatialAnchors::ISessionConfiguration
{
    SessionConfiguration(std::nullptr_t) noexcept {}
};

struct WINRT_EBO SessionErrorEventArgs :
    Microsoft::Azure::SpatialAnchors::ISessionErrorEventArgs
{
    SessionErrorEventArgs(std::nullptr_t) noexcept {}
};

struct WINRT_EBO SessionStatus :
    Microsoft::Azure::SpatialAnchors::ISessionStatus
{
    SessionStatus(std::nullptr_t) noexcept {}
};

struct WINRT_EBO SessionUpdatedEventArgs :
    Microsoft::Azure::SpatialAnchors::ISessionUpdatedEventArgs
{
    SessionUpdatedEventArgs(std::nullptr_t) noexcept {}
};

struct WINRT_EBO SpatialAnchorsFactory :
    Microsoft::Azure::SpatialAnchors::ISpatialAnchorsFactory
{
    SpatialAnchorsFactory(std::nullptr_t) noexcept {}
};

struct WINRT_EBO TokenRequiredEventArgs :
    Microsoft::Azure::SpatialAnchors::ITokenRequiredEventArgs
{
    TokenRequiredEventArgs(std::nullptr_t) noexcept {}
};

}
